library verilog;
use verilog.vl_types.all;
entity pipe_exe is
    port(
        rd1             : in     vl_logic_vector(31 downto 0);
        rd2             : in     vl_logic_vector(31 downto 0);
        shamt32         : in     vl_logic_vector(31 downto 0);
        shift           : in     vl_logic;
        aluc            : in     vl_logic_vector(3 downto 0);
        alud            : out    vl_logic_vector(31 downto 0);
        zero            : out    vl_logic;
        carry           : out    vl_logic;
        negative        : out    vl_logic;
        overflow        : out    vl_logic
    );
end pipe_exe;
