`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    19:16:03 04/22/2013 
// Design Name: 
// Module Name:    controlunit 
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
module controlunit(
		input [5:0] op,
		input [5:0] func,
		output [3:0] aluc,
		output wrf,
		output sext,
		output shift,
		output [1:0] pcsource
    );

wire r_type;
wire i_add, i_addu, i_sub, i_subu, i_and, i_or, i_xor, i_nor, i_slt, i_sltu, i_sll, i_srl, i_sra,
	  i_sllv, i_srlv, i_srav, i_jr;

assign r_type = ~op[5] && ~op[4] && ~op[3] && ~op[2] && ~op[1] && ~op[0];
assign i_add  = r_type &&  func[5] && ~func[4] && ~func[3] && ~func[2] && ~func[1] && ~func[0];
assign i_addu = r_type &&  func[5] && ~func[4] && ~func[3] && ~func[2] && ~func[1] &&  func[0];
assign i_sub  = r_type &&  func[5] && ~func[4] && ~func[3] && ~func[2] &&  func[1] && ~func[0];
assign i_subu = r_type &&  func[5] && ~func[4] && ~func[3] && ~func[2] &&  func[1] &&  func[0];
assign i_and  = r_type &&  func[5] && ~func[4] && ~func[3] &&  func[2] && ~func[1] && ~func[0];
assign i_or   = r_type &&  func[5] && ~func[4] && ~func[3] &&  func[2] && ~func[1] &&  func[0];
assign i_xor  = r_type &&  func[5] && ~func[4] && ~func[3] &&  func[2] &&  func[1] && ~func[0];
assign i_nor  = r_type &&  func[5] && ~func[4] && ~func[3] &&  func[2] &&  func[1] &&  func[0];
assign i_slt  = r_type &&  func[5] && ~func[4] &&  func[3] && ~func[2] &&  func[1] && ~func[0];
assign i_sltu = r_type &&  func[5] && ~func[4] &&  func[3] && ~func[2] &&  func[1] &&  func[0];
assign i_sll  = r_type && ~func[5] && ~func[4] && ~func[3] && ~func[2] && ~func[1] && ~func[0];
assign i_srl  = r_type && ~func[5] && ~func[4] && ~func[3] && ~func[2] &&  func[1] && ~func[0];
assign i_sra  = r_type && ~func[5] && ~func[4] && ~func[3] && ~func[2] &&  func[1] && ~func[0];
assign i_sllv = r_type && ~func[5] && ~func[4] && ~func[3] &&  func[2] && ~func[1] && ~func[0];
assign i_srlv = r_type && ~func[5] && ~func[4] && ~func[3] &&  func[2] &&  func[1] && ~func[0];
assign i_srav = r_type && ~func[5] && ~func[4] && ~func[3] &&  func[2] &&  func[1] &&  func[0];
assign i_jr   = r_type && ~func[5] && ~func[4] &&  func[3] && ~func[2] && ~func[1] && ~func[0];

//alu�Ŀ���
assign aluc[0] = i_subu || i_sub || i_or  || i_nor  || i_srl || i_srlv || i_slt;
assign aluc[1] = i_add  || i_sub || i_xor || i_nor  || i_sll || i_sllv || i_slt || i_sltu;
assign aluc[2] = i_and  || i_or  || i_xor || i_nor  || i_sra || i_srav || i_sll || i_sllv || i_srl || i_srlv;
assign aluc[3] = i_sra  || i_srav|| i_sll || i_sllv || i_srl || i_srlv || i_slt || i_sltu;
//д�Ĵ������ź�
assign wrf = i_add || i_addu || i_sub || i_subu || i_and  || i_or  || i_xor || i_nor || i_slt || i_sltu || 
				 i_sll || i_srl  || i_sra || i_sllv || i_srlv || i_srav;
//��չ�����źţ��ߵ�ƽ������λ��չ�� �͵�ƽ������չ
assign sext = i_sll || i_srl || i_sra;
//aluԴ�����������źţ��ߵ�ƽ: Դ������������λ��չ���룬 �͵�ƽ��Դ����������rf��rd1���
assign shift = i_sll || i_srl || i_sra;
//npcѡ���źţ�00��pc+4�� 01��jr��npc������rf��Դ��������
assign pcsource[0] = i_jr; 
assign pcsource[1] = 0;
endmodule
