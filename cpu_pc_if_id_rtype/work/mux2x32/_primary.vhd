library verilog;
use verilog.vl_types.all;
entity mux2x32 is
    port(
        a               : in     vl_logic_vector(31 downto 0);
        b               : in     vl_logic_vector(31 downto 0);
        \select\        : in     vl_logic;
        r               : out    vl_logic_vector(31 downto 0)
    );
end mux2x32;
