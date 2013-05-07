`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   09:46:51 04/16/2013
// Design Name:   clk_div
// Module Name:   E:/fpga_svn/mem/clk_div_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: clk_div
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module clk_div_tb;

	// Inputs
	reg clk;
	reg rst;

	// Outputs
	wire CPUCLK;

	// Instantiate the Unit Under Test (UUT)
	clk_div uut (
		.clk(clk), 
		.rst(rst), 
		.CPUCLK(CPUCLK)
	);
	
	localparam T = 20;
	
	initial begin
		rst = 1;
		#(T/2);
		rst = 0;
	end
	
	initial begin
		// Initialize Inputs
		clk = 0;
		rst = 0;

		// Wait 100 ns for global reset to finish
		#100;
        
		// Add stimulus here

	end
      
	always begin
		clk = 0;
		#(T/2);
		clk = 1;
		#(T/2);
	end
endmodule

