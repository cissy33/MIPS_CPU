`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   15:02:19 04/22/2013
// Design Name:   top_mem
// Module Name:   E:/fpga_svn/mem/top_mem_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: top_mem
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module top_mem_tb;

	// Inputs
	reg clk;
	reg rst;
	reg [31:0] ram_indata;

	// Instantiate the Unit Under Test (UUT)
	top_mem uut (
		.clk(clk), 
		.rst(rst), 
		.ram_indata(ram_indata)
	);

   localparam T = 20;
	
	always begin
		clk = 0;
		#(T/2);
		clk = 1;
		#(T/2);
	end
	
	initial begin
		// Initialize Inputs
		clk = 0;
		rst = 1;
		ram_indata = 0;

		// Wait 100 ns for global reset to finish
		#100;
        
		// Add stimulus here
		rst = 0;
	end
      
endmodule

