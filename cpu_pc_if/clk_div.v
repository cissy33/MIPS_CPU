`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    21:58:44 04/15/2013 
// Design Name: 
// Module Name:    clk_div 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//			奇数分频器 50%占空比
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module clk_div(
		input clk,
		input rst,
		output CPUCLK
    );

	//2N+1奇数分频器50%占空比
	reg [2:0] clkcount_pos_r, clkcount_neg_r;
	reg clk_pos;
	reg clk_neg;
	always @(posedge clk or posedge rst) begin  //对clk的上升沿 2N-1 采样计数
		if(rst) begin
			clkcount_pos_r <= 3'b0;
		end else if(clkcount_pos_r < 6) begin
			clkcount_pos_r <= clkcount_pos_r + 3'b1;
		end else
			clkcount_pos_r <= 3'b0;
	end
	
	always @(posedge clk or posedge rst) begin //对clk上升沿 N/2N+1 进行分频得到上升沿分频时钟信号
		if(rst) begin
			clk_pos <= 1'b0;
		end else if(clkcount_pos_r < 3) begin
			clk_pos <= 1'b1;
		end else begin
			clk_pos <= 1'b0;
		end
	end
	
	always @(negedge clk or posedge rst) begin
		if(rst) begin
			clkcount_neg_r <= 3'b0;
		end else if(clkcount_neg_r < 6) begin
			clkcount_neg_r <= clkcount_neg_r + 3'b1;
		end else
			clkcount_neg_r <= 3'b0;
	end
	
	always @(negedge clk or posedge rst) begin
		if(rst) begin
			clk_neg <= 1'b0;
		end else if(clkcount_neg_r < 3) begin
			clk_neg <= 1'b1;
		end else begin
			clk_neg <= 1'b0;
		end
	end
	
	assign CPUCLK = clk_pos || clk_neg; //将上升沿分频时钟和下降沿分频时钟进行或运算得到50%占空比的分频时钟

endmodule
