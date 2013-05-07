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
		//�޸�imemʱ������
		input [31:0] ram_indata
    );

wire [31:0] pc, npc;
//------------------------�����ź�---------------------------
//ram_ena: ָ��Ĵ���ʹ���źţ� ��Ϊ�ߵ�ƽʹ��
//ram_wean: ָ��Ĵ���д�źţ�����debugʱ�޸�ָ��Ĵ����ڲ���ֵ
//pc_ena��pc�Ĵ���ʹ���źţ��ߵ�ƽ��Ч������ˮ��stallʱ��ʹ��͵�ƽ��Ч
//rf_ena���ڻ�д�׶Σ���Ҫд�Ĵ��������д�ź�
//wrf: ������׶Σ��õ���д�Ĵ������ź�
//shift��������׶Σ��õ�������д��alua�˵�ѡ���źţ��͵�ƽ��ѡ��Ĵ���������rd1���ߵ�ƽ��ѡ����������չ���ֵ
//aluc: ������׶Σ��õ���alu�Ŀ����ź�
//shift_e: ��ִ�н׶Σ�alua��Ҫ�Ŀ����ź�
//aluc_e�� ��ִ�н׶Σ�alu��Ҫ�Ŀ����ź�
wire ram_ena, ram_wena, pc_ena; 
wire rf_wena;
wire wrf, shift;
wire [3:0] aluc;
wire [1:0] pcsource;
wire shift_e;
wire [3:0] aluc_e;
//------------------------��������---------------------------
//instr: ��ȡָ�׶Σ���ȡָ�Ԫ�õ���ָ��
//pc_jr: ��ȡֵ�׶Σ�������׶εõ��ĴӼĴ�����õ���jr����ת�ĵ�ַ������һ��npc��ֵ
//wrf_data���ڻ�д�׶Σ�����д�뵽�Ĵ����������
//wrf_addr: ��д�׶�д�Ĵ����ĵ�ַ
//rd1, rd2: ������׶�ȡ����Դ������a��b
//shamt32: ����׶�instr[10:6]����������չֵ
//alud: ��ִ�н׶Σ�alu�ó���������
//zero, carry, overflow, negative��ִ�н׶εõ��ı�־λ
//wrf_addr_d: ����׶Σ��õ�Ҫ��д�ļĴ�����ַ
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
