//--------------------------------------------------------------
// tjbus.v
// bus for bridging the tjmips32 cpu and ios/memory
// Globalrenzhi@gmail.com
//-------------------------------------------------------------

// bus module
module bus(
    input   [31:0]  c_address,
    input           c_we,
    input   [31:0]  c_wdata,
    output  [31:0]  c_rdata,
    output  [11:0]  io_address,//address bus
    output  [4:0]   io_cs,//chip select, enable sigal for io device
									//io_cs[3:0]for io, io_cs[4] for dram 
    output          io_we,//write enable
    output  [31:0]  io_wdata,//data bus
    input   [31:0]  io_rdata_de0,
	 input   [31:0]  io_rdata_de1,
	 input   [31:0]  io_rdata_de2,
	 input   [31:0]  io_rdata_de3,
	 input	[31:0]  io_rdata_mem
    );
   
	assign io_address = c_address[11:0];
   assign io_we = c_we;
   assign io_cs[0] = (c_address[15:8] == 8'h00) ? 1'b1 : 1'b0;//device 0:0x0000-0x00ff
   assign io_cs[1] = (c_address[15:8] == 8'h01) ? 1'b1 : 1'b0;//device 1:0x0100-0x01ff
   assign io_cs[2] = (c_address[15:8] == 8'h02) ? 1'b1 : 1'b0;//device 2:0x0200-0x02ff
   assign io_cs[3] = (c_address[15:8] == 8'h03) ? 1'b1 : 1'b0;//device 3:0x0300-0x03ff
   assign io_cs[4] = (c_address[15:12] == 4'h1) ? 1'b1 : 1'b0;//dmem:0x1000-0x1fff
  
	assign io_wdata = c_wdata;
   assign c_rdata = io_cs[4] ? (io_rdata_mem):(io_cs[3] ? (io_rdata_de3):(io_cs[2] ? (io_rdata_de2):(io_cs[1] ? (io_rdata_de1):(io_cs[0] ? (io_rdata_de0):(32'hz)))));
endmodule
