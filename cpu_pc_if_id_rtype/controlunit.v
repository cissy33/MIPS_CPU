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

//alu的控制
assign aluc[0] = i_subu || i_sub || i_or  || i_nor  || i_srl || i_srlv || i_slt;
assign aluc[1] = i_add  || i_sub || i_xor || i_nor  || i_sll || i_sllv || i_slt || i_sltu;
assign aluc[2] = i_and  || i_or  || i_xor || i_nor  || i_sra || i_srav || i_sll || i_sllv || i_srl || i_srlv;
assign aluc[3] = i_sra  || i_srav|| i_sll || i_sllv || i_srl || i_srlv || i_slt || i_sltu;
//写寄存器组信号
assign wrf = i_add || i_addu || i_sub || i_subu || i_and  || i_or  || i_xor || i_nor || i_slt || i_sltu || 
				 i_sll || i_srl  || i_sra || i_sllv || i_srlv || i_srav;
//扩展控制信号，高电平：符号位扩展， 低电平：零扩展
assign sext = i_sll || i_srl || i_sra;
//alu源操作数控制信号，高电平: 源操作数来自移位扩展输入， 低电平：源操作数来自rf的rd1输出
assign shift = i_sll || i_srl || i_sra;
//npc选择信号，00：pc+4， 01：jr（npc来自于rf的源操作数）
assign pcsource[0] = i_jr; 
assign pcsource[1] = 0;
endmodule
