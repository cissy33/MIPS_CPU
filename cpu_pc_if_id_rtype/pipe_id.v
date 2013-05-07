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
		input [31:0] instr, //��ȡָ�׶εõ��ĵ�ǰָ��
		input [31:0] wrf_data, //��д�׶�д�Ĵ�����ֵ
		input rf_wena, //��д�׶�д�Ĵ�����д�ź�
		input  [4:0]  rf_waddr, //��д�׶�д�Ĵ����ĵ�ַ
		output [31:0] rd1, //����׶Σ��ӼĴ�����õ���Դ������a
		output [31:0] rd2, //����׶Σ��ӼĴ�����õ���Դ������b
		output [31:0] shamt32, //����׶Σ�ָ��shamt������չ�õ���32λ������
		output [4:0] rd, //����׶Σ��õ�Ҫд�Ĵ�����ĵ�ַ
		//���Ƶ�Ԫ�ź�
		output [3:0] aluc, //����׶Σ��ӿ��Ƶ�Ԫ�õ���alu�Ŀ����ź�
		output wrf, //����׶Σ��ӿ��Ƶ�Ԫ�õ��Ļ�д�Ĵ�����д�ź�
		output shift,
		output [1:0] pcsource
    );

wire [4:0] ra1, ra2, wa;
wire [31:0] wd;
wire [4:0] rs, rt, shamt;
wire [5:0] op, func;
wire sext;

//�Ĵ�����clk�½���д�����ݣ���֤������ִ�н���
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
