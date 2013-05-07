library verilog;
use verilog.vl_types.all;
entity ext is
    port(
        a               : in     vl_logic_vector(4 downto 0);
        sext            : in     vl_logic;
        b               : out    vl_logic_vector(31 downto 0)
    );
end ext;
