library verilog;
use verilog.vl_types.all;
entity controlunit is
    port(
        op              : in     vl_logic_vector(5 downto 0);
        func            : in     vl_logic_vector(5 downto 0);
        aluc            : out    vl_logic_vector(3 downto 0);
        wrf             : out    vl_logic;
        sext            : out    vl_logic;
        shift           : out    vl_logic;
        pcsource        : out    vl_logic_vector(1 downto 0)
    );
end controlunit;
