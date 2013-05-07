`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    19:26:04 04/15/2013 
// Design Name: 
// Module Name:    top_mem 
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
module top_mem(
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
wire ram_ena, ram_wena, pc_ena;
wire [31:0] instr;

dffe #(32) pcreg(clk, rst, pc_ena, npc, pc);
pipe_if pipeif(clk, pc, ram_ena, ram_wena, ram_indata, instr, npc);


assign ram_ena = 1'b1;
assign ram_wena = 1'b0;
assign pc_ena = 1'b1;


endmodule
