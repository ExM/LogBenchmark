reset
set xlabel "time [ns]"
set key below
plot \
'NLog_dotNet_N3_1.dat' with impulses, \
'NLog_dotNet_N1_1.dat' with impulses, \
'NLog_dotNet_N0_100.dat' with impulses, \
'NLog_dotNet_N0_1.dat' with impulses





