library verilog;
use verilog.vl_types.all;
entity logarithm is
    port(
        N               : in     vl_logic_vector(31 downto 0);
        bitnum          : out    vl_logic_vector(4 downto 0)
    );
end logarithm;
