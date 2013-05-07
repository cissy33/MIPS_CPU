`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    21:31:04 04/24/2013 
// Design Name: 
// Module Name:    pipe_exe 
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
module pipe_exe(
		input [31:0] rd1,
		input [31:0] rd2,
		input [31:0] shamt32,
		input shift,
		input [3:0] aluc,
		output[31:0] alud,
		output zero,
		output carry,
		output negative,
		output overflow
    );
wire [31:0] alua, alub;

alu alu(alua, alub, aluc, alud, zero, carry, negative, overflow);
mux2x32 select_alua(rd1, shamt32, shift, alua);

assign alub = rd2;

endmodule
