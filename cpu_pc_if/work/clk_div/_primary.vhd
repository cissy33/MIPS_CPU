library verilog;
use verilog.vl_types.all;
entity clk_div is
    port(
        clk             : in     vl_logic;
        rst             : in     vl_logic;
        CPUCLK          : out    vl_logic
    );
end clk_div;
