`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    18:52:13 04/22/2013 
// Design Name: 
// Module Name:    pipe_id 
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
module pipe_id(
		input clk,
		input rst,
		input [31:0] instr, //从取指阶段得到的当前指令
		input [31:0] wrf_data, //回写阶段写寄存器的值
		input rf_wena, //回写阶段写寄存器的写信号
		input  [4:0]  rf_waddr, //回写阶段写寄存器的地址
		output [31:0] rd1, //译码阶段，从寄存器组得到的源操作数a
		output [31:0] rd2, //译码阶段，从寄存器组得到的源操作数b
		output [31:0] shamt32, //译码阶段，指令shamt经过扩展得到的32位立即数
		output [4:0] rd, //译码阶段，得到要写寄存器组的地址
		//控制单元信号
		output [3:0] aluc, //译码阶段，从控制单元得到的alu的控制信号
		output wrf, //译码阶段，从控制单元得到的回写寄存器的写信号
		output shift,
		output [1:0] pcsource
    );

wire [4:0] ra1, ra2, wa;
wire [31:0] wd;
wire [4:0] rs, rt, shamt;
wire [5:0] op, func;
wire sext;

//寄存器组clk下降沿写入数据，保证单周期执行结束
regfiles rf(clk, rst, rf_wena, ra1, ra2, wa, wd, rd1, rd2);
controlunit cu(op, func, aluc, wrf, sext, shift, pcsource);
ext shamtext(shamt, sext, shamt32);

assign rs = instr[25:21];
assign rt = instr[20:16];
assign rd = instr[15:11];
assign shamt = instr[10:6];
assign op = instr[31:26];
assign func = instr[5:0];

assign ra1 = rs;
assign ra2 = rt;
assign wa = rf_waddr;
assign wd = wrf_data;


endmodule
