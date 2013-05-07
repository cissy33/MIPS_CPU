`timescale 1ns / 1ps

////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer:
//
// Create Date:   14:25:49 04/22/2013
// Design Name:   pipe_if
// Module Name:   E:/fpga_svn/mem/pipe_if_tb.v
// Project Name:  mem
// Target Device:  
// Tool versions:  
// Description: 
//
// Verilog Test Fixture created by ISE for module: pipe_if
//
// Dependencies:
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
////////////////////////////////////////////////////////////////////////////////

module pipe_if_tb;

	// Inputs
	reg clk;
	reg [31:0] pc;
	reg ram_ena;
	reg ram_wena;
	reg [31:0] ram_indata;

	// Outputs
	wire [31:0] ram_outdata;
	wire [31:0] npc;

	integer k;
	// Instantiate the Unit Under Test (UUT)
	pipe_if uut (
	   .clk(clk),
		.pc(pc), 
		.ram_ena(ram_ena), 
		.ram_wena(ram_wena), 
		.ram_indata(ram_indata), 
		.ram_outdata(ram_outdata), 
		.npc(npc)
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
		pc = 0;
		ram_ena = 1;
		ram_wena = 0;
		ram_indata = 0;

		// Wait 100 ns for global reset to finish
		#100;
        
		// Add stimulus here
		for(k = 0; k < 5; k = k + 1) begin
			pc = npc;
			#(T);
		end
	end
      
endmodule

