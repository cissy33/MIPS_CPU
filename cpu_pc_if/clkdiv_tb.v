`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   16:06:01 04/16/2013
// Design Name:   clkdiv
// Module Name:   E:/fpga_svn/mem/clkdiv_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: clkdiv
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module clkdiv_tb;

	// Inputs
	reg clk;
	reg rst;
	reg SW;

	// Outputs
	wire CPUCLK;

	// Instantiate the Unit Under Test (UUT)
	clkdiv uut (
		.clk(clk), 
		.rst(rst), 
		.SW(SW), 
		.CPUCLK(CPUCLK)
	);

	localparam T = 20;
	initial begin
		rst = 1;
		#(T/2)
		rst = 0;
	end
	
	initial begin
		// Initialize Inputs
		SW = 0;

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

