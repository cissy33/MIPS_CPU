`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   10:49:51 04/21/2013
// Design Name:   ram
// Module Name:   E:/fpga_svn/mem/ram_writefirst_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: ram
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module ram_writefirst_tb;

	// Inputs
	reg clk;
	reg ram_ena;
	reg wena;
	reg [2:0] addr;
	reg [7:0] data_in;

	// Outputs
	wire [7:0] data_out;

	// Instantiate the Unit Under Test (UUT)
	ram uut (
		.clk(clk), 
		.ram_ena(ram_ena), 
		.wena(wena), 
		.addr(addr), 
		.data_in(data_in), 
		.data_out(data_out)
	);

	localparam T = 20;
	
	initial begin
		// Initialize Inputs
		ram_ena = 1;
		wena = 0;
		addr = 0;
		data_in = 0;

		// Wait 100 ns for global reset to finish
		#100;
        
		// Add stimulus here
		@(negedge clk)
		addr = 0;
		data_in = 8'd3;
		wena = 1;
		#(T);
		wena = 0;
		#(T);
		
	end
	
	always begin
		clk = 0;
		#(T/2);
		clk = 1;
		#(T/2);
	end
      
endmodule

