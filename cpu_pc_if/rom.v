`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    15:10:42 04/08/2013 
// Design Name: 
// Module Name:    rom 
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
module rom(
		input [4:0] addr,
		output [31:0] data
    );

reg [31:0] irom [2**5 - 1: 0];

initial begin
	$readmemh("memfile.tv", irom);
end

assign data = irom[addr];
endmodule
