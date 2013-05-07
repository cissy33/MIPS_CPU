`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    18:51:15 04/22/2013 
// Design Name: 
// Module Name:    top_cpu 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module top_cpu(
		input clk,
		input rst,
		//修改imem时的数据
		input [31:0] ram_indata
    );

wire [31:0] pc, npc;
//------------------------控制信号---------------------------
//ram_ena: 指令寄存器使能信号， 常为高电平使能
//ram_wean: 指令寄存器写信号，用于debug时修改指令寄存器内部的值
//pc_ena：pc寄存器使能信号，高电平有效，在流水线stall时，使其低电平无效
//rf_ena：在回写阶段，需要写寄存器组给的写信号
//wrf: 在译码阶段，得到的写寄存器组信号
//shift：在译码阶段，得到的最终写入alua端的选择信号，低电平：选择寄存器读出的rd1，高电平：选择立即数扩展后的值
//aluc: 在译码阶段，得到的alu的控制信号
//shift_e: 在执行阶段，alua需要的控制信号
//aluc_e： 在执行阶段，alu需要的控制信号
wire ram_ena, ram_wena, pc_ena; 
wire rf_wena;
wire wrf, shift;
wire [3:0] aluc;
wire [1:0] pcsource;
wire shift_e;
wire [3:0] aluc_e;
//------------------------数据总线---------------------------
//instr: 在取指阶段，从取指令单元得到的指令
//pc_jr: 在取值阶段，在译码阶段得到的从寄存器组得到的jr的跳转的地址，其中一个npc的值
//wrf_data：在回写阶段，最终写入到寄存器组的数据
//wrf_addr: 回写阶段写寄存器的地址
//rd1, rd2: 在译码阶段取出的源操作数a和b
//shamt32: 译码阶段instr[10:6]立即数的扩展值
//alud: 在执行阶段，alu得出的运算结果
//zero, carry, overflow, negative：执行阶段得到的标志位
//wrf_addr_d: 译码阶段，得到要回写的寄存器地址
wire [31:0] instr;
wire [31:0] pc_jr;
wire [31:0] wrf_data;
wire [4:0] wrf_addr;
wire [31:0] rd1, rd2, shamt32;
wire [31:0] alud;
wire zero, carry, overflow, negative;
wire [4:0] wrf_addr_d;


dffe #(32) pcreg(clk, rst, pc_ena, npc, pc);
pipe_if pipeif(clk, pc, ram_ena, ram_wena, ram_indata, pc_jr, pcsource, instr, npc);
pipe_id pipeid(clk, rst, instr, wrf_data, rf_wena, wrf_addr, rd1, rd2, shamt32, wrf_addr_d, aluc, wrf, shift, pcsource);
pipe_exe pipeexe(rd1, rd2, shamt32, shift_e, aluc_e, alud, zero, carry, negative, overflow);


assign ram_ena = 1'b0;
assign ram_wena = 1'b0;
assign pc_ena = 1'b1;
assign rf_wena = wrf;
assign aluc_e = aluc;
assign shift_e = shift;
assign pc_jr = rd1;
assign wrf_data = alud;
assign wrf_addr = wrf_addr_d;


endmodule
