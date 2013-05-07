library verilog;
use verilog.vl_types.all;
entity pipe_id is
    port(
        clk             : in     vl_logic;
        rst             : in     vl_logic;
        instr           : in     vl_logic_vector(31 downto 0);
        wrf_data        : in     vl_logic_vector(31 downto 0);
        rf_wena         : in     vl_logic;
        rf_waddr        : in     vl_logic_vector(4 downto 0);
        rd1             : out    vl_logic_vector(31 downto 0);
        rd2             : out    vl_logic_vector(31 downto 0);
        shamt32         : out    vl_logic_vector(31 downto 0);
        rd              : out    vl_logic_vector(4 downto 0);
        aluc            : out    vl_logic_vector(3 downto 0);
        wrf             : out    vl_logic;
        shift           : out    vl_logic;
        pcsource        : out    vl_logic_vector(1 downto 0)
    );
end pipe_id;
