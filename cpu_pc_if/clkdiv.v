`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    11:10:28 04/16/2013 
// Design Name: 
// Module Name:    clkdiv 
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
module clkdiv #(parameter N = 2)( //偶数分频 50% 占空比
		input clk,
		input rst,
		input SW,
		output CPUCLK
    );

	
	reg [31:0] clkcount_r;
	reg clkdiv_r;
	always @(posedge clk or posedge rst) begin
		if(rst) begin
			clkcount_r <= 1'b0;
		end else if(clkcount_r < N-1)begin
			clkcount_r <= clkcount_r + 1;
		end
			clkcount_r <= 0;
	end
	
	always @(posedge clk or posedge rst) begin
		if(rst) begin
			clkdiv_r <= 0;
		end else if(clkcount_r < N/2 - 1) begin
			clkdiv_r <= 0;
		end else 
			clkdiv_r <= 1;
	end
	
	assign CPUCLK = SW ? clkcount[0] : clkdiv_r;
endmodule
