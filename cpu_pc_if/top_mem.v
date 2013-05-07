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
		//�޸�imemʱ������
		input [31:0] ram_indata
    );

wire [31:0] pc, npc;
//------------------------�����ź�---------------------------
//ram_ena: ָ��Ĵ���ʹ���źţ� ��Ϊ�ߵ�ƽʹ��
//ram_wean: ָ��Ĵ���д�źţ�����debugʱ�޸�ָ��Ĵ����ڲ���ֵ
//pc_ena��pc�Ĵ���ʹ���źţ��ߵ�ƽ��Ч������ˮ��stallʱ��ʹ��͵�ƽ��Ч
wire ram_ena, ram_wena, pc_ena;
wire [31:0] instr;

dffe #(32) pcreg(clk, rst, pc_ena, npc, pc);
pipe_if pipeif(clk, pc, ram_ena, ram_wena, ram_indata, instr, npc);


assign ram_ena = 1'b1;
assign ram_wena = 1'b0;
assign pc_ena = 1'b1;


endmodule
