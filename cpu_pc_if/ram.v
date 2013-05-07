`timescale 1ns / 1ps

//`define WRITE_FIRST 
//`define READ_FIRST 
`define NO_CHANGE 

//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    09:19:09 04/21/2013 
// Design Name: 
// Module Name:    ram 
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
module ram #(parameter WIDTH = 8, DEPTH = 3)(
		input clk,
		input ram_ena,
		input wena,
		input [DEPTH - 1:0] addr,
		input [WIDTH - 1:0] data_in,
		output reg [WIDTH - 1:0] data_out
    );

   parameter RAM_WIDTH = WIDTH;
   parameter RAM_ADDR_BITS = DEPTH;
   
   (* RAM_STYLE="{AUTO | BLOCK |  BLOCK_POWER1 | BLOCK_POWER2}" *)
   reg [RAM_WIDTH-1:0] ram [(2**RAM_ADDR_BITS)-1:0];

   //  The following code is only necessary if you wish to initialize the RAM 
   //  contents via an external file (use $readmemb for binary data)
   initial begin
	   $readmemb("memfile.tv", ram);
	end
	
	`ifdef WRITE_FIRST
		always @(posedge clk)
      if (ram_ena) begin
         if (wena) begin
            ram[addr] <= data_in;
            data_out <= data_in;
         end
         else
            data_out <= ram[addr];
      end
	`elsif READ_FIRST
		always @(posedge clk)
      if (ram_ena) begin
         if (wena)
            ram[addr] <= data_in;
         data_out <= ram[addr];
      end
	`elsif NO_CHANGE
		always @(posedge clk)
      if (ram_ena)
         if (wena)
            ram[addr] <= data_in;
         else
            data_out <= ram[addr];
	`endif
						
endmodule
