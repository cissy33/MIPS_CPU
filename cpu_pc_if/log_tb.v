`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   13:03:12 04/16/2013
// Design Name:   logarithm
// Module Name:   E:/fpga_svn/mem/log_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: logarithm
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module log_tb;

	// Inputs
	reg [31:0] N;

	// Outputs
	wire [4:0] bitnum;
	
	integer k;

	// Instantiate the Unit Under Test (UUT)
	logarithm uut (
		.N(N), 
		.bitnum(bitnum)
	);

	localparam T = 5;
	initial begin
//		// Initialize Inputs
//		N = 0;
//
//		// Wait 100 ns for global reset to finish
//		#100;
//        
//		// Add stimulus here
		N = 0;
		#T;
		
		for(k = 0; k < 20; k = k + 1) begin
			N = N + 1;
			#T;
		end
//		N = 32'd0;
//		#5;
//		N = 32'd1;
//		#5;
//		N = 32'd2;
	end
      
endmodule

