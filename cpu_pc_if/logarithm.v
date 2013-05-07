`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    11:28:18 04/16/2013 
// Design Name: 
// Module Name:    logarithm 
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
module logarithm (
		input [31:0] N,
		output [4:0] bitnum
    );

	reg [4:0] count = 5'b0; 
	reg [31:0] temp;
	always @(*) begin
		temp  = N;
		count = 0;
		if(N == 32'b0 || N == 32'b1)
			count = 5'b1;
		else
			while(temp[31:1] != 31'b0 || temp[0] != 1'b1) begin
				temp = temp >> 1;
				count = count + 5'b1;
			end
			
		if(N >= 2**count)
			count = count + 5'b1;
	end
	
	assign bitnum = count;
endmodule
