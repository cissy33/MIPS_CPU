`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    13:58:31 04/22/2013 
// Design Name: 
// Module Name:    pipe_if 
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
module pipe_if(
		input clk,
		input [31:0] pc, //��ǰpcֵ
		//ram_ena, ram_wena, ram_indata���޸�ָ��Ĵ���ʱʹ��
		input ram_ena,
		input ram_wena,
		input [31:0] ram_indata,
		output [31:0] ram_outdata, //ͨ��pcֵ���޸Ĵ�ָ��洢����ȡ����ǰָ��
		output [31:0] npc //��һ��pcֵ
    );

wire [31:0] npc4; //pc+4���������һ��pcֵ

ram #(32, 3) iram(clk, ram_ena, ram_wena, pc[4:2], ram_indata, ram_outdata);
top_cla_32 pcplus4(pc, 32'd4, 1'b0, npc4);

assign npc = npc4;

endmodule
