using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000627 RID: 1575
	internal enum OpCodeValues
	{
		// Token: 0x04001EB1 RID: 7857
		Nop,
		// Token: 0x04001EB2 RID: 7858
		Break,
		// Token: 0x04001EB3 RID: 7859
		Ldarg_0,
		// Token: 0x04001EB4 RID: 7860
		Ldarg_1,
		// Token: 0x04001EB5 RID: 7861
		Ldarg_2,
		// Token: 0x04001EB6 RID: 7862
		Ldarg_3,
		// Token: 0x04001EB7 RID: 7863
		Ldloc_0,
		// Token: 0x04001EB8 RID: 7864
		Ldloc_1,
		// Token: 0x04001EB9 RID: 7865
		Ldloc_2,
		// Token: 0x04001EBA RID: 7866
		Ldloc_3,
		// Token: 0x04001EBB RID: 7867
		Stloc_0,
		// Token: 0x04001EBC RID: 7868
		Stloc_1,
		// Token: 0x04001EBD RID: 7869
		Stloc_2,
		// Token: 0x04001EBE RID: 7870
		Stloc_3,
		// Token: 0x04001EBF RID: 7871
		Ldarg_S,
		// Token: 0x04001EC0 RID: 7872
		Ldarga_S,
		// Token: 0x04001EC1 RID: 7873
		Starg_S,
		// Token: 0x04001EC2 RID: 7874
		Ldloc_S,
		// Token: 0x04001EC3 RID: 7875
		Ldloca_S,
		// Token: 0x04001EC4 RID: 7876
		Stloc_S,
		// Token: 0x04001EC5 RID: 7877
		Ldnull,
		// Token: 0x04001EC6 RID: 7878
		Ldc_I4_M1,
		// Token: 0x04001EC7 RID: 7879
		Ldc_I4_0,
		// Token: 0x04001EC8 RID: 7880
		Ldc_I4_1,
		// Token: 0x04001EC9 RID: 7881
		Ldc_I4_2,
		// Token: 0x04001ECA RID: 7882
		Ldc_I4_3,
		// Token: 0x04001ECB RID: 7883
		Ldc_I4_4,
		// Token: 0x04001ECC RID: 7884
		Ldc_I4_5,
		// Token: 0x04001ECD RID: 7885
		Ldc_I4_6,
		// Token: 0x04001ECE RID: 7886
		Ldc_I4_7,
		// Token: 0x04001ECF RID: 7887
		Ldc_I4_8,
		// Token: 0x04001ED0 RID: 7888
		Ldc_I4_S,
		// Token: 0x04001ED1 RID: 7889
		Ldc_I4,
		// Token: 0x04001ED2 RID: 7890
		Ldc_I8,
		// Token: 0x04001ED3 RID: 7891
		Ldc_R4,
		// Token: 0x04001ED4 RID: 7892
		Ldc_R8,
		// Token: 0x04001ED5 RID: 7893
		Dup = 37,
		// Token: 0x04001ED6 RID: 7894
		Pop,
		// Token: 0x04001ED7 RID: 7895
		Jmp,
		// Token: 0x04001ED8 RID: 7896
		Call,
		// Token: 0x04001ED9 RID: 7897
		Calli,
		// Token: 0x04001EDA RID: 7898
		Ret,
		// Token: 0x04001EDB RID: 7899
		Br_S,
		// Token: 0x04001EDC RID: 7900
		Brfalse_S,
		// Token: 0x04001EDD RID: 7901
		Brtrue_S,
		// Token: 0x04001EDE RID: 7902
		Beq_S,
		// Token: 0x04001EDF RID: 7903
		Bge_S,
		// Token: 0x04001EE0 RID: 7904
		Bgt_S,
		// Token: 0x04001EE1 RID: 7905
		Ble_S,
		// Token: 0x04001EE2 RID: 7906
		Blt_S,
		// Token: 0x04001EE3 RID: 7907
		Bne_Un_S,
		// Token: 0x04001EE4 RID: 7908
		Bge_Un_S,
		// Token: 0x04001EE5 RID: 7909
		Bgt_Un_S,
		// Token: 0x04001EE6 RID: 7910
		Ble_Un_S,
		// Token: 0x04001EE7 RID: 7911
		Blt_Un_S,
		// Token: 0x04001EE8 RID: 7912
		Br,
		// Token: 0x04001EE9 RID: 7913
		Brfalse,
		// Token: 0x04001EEA RID: 7914
		Brtrue,
		// Token: 0x04001EEB RID: 7915
		Beq,
		// Token: 0x04001EEC RID: 7916
		Bge,
		// Token: 0x04001EED RID: 7917
		Bgt,
		// Token: 0x04001EEE RID: 7918
		Ble,
		// Token: 0x04001EEF RID: 7919
		Blt,
		// Token: 0x04001EF0 RID: 7920
		Bne_Un,
		// Token: 0x04001EF1 RID: 7921
		Bge_Un,
		// Token: 0x04001EF2 RID: 7922
		Bgt_Un,
		// Token: 0x04001EF3 RID: 7923
		Ble_Un,
		// Token: 0x04001EF4 RID: 7924
		Blt_Un,
		// Token: 0x04001EF5 RID: 7925
		Switch,
		// Token: 0x04001EF6 RID: 7926
		Ldind_I1,
		// Token: 0x04001EF7 RID: 7927
		Ldind_U1,
		// Token: 0x04001EF8 RID: 7928
		Ldind_I2,
		// Token: 0x04001EF9 RID: 7929
		Ldind_U2,
		// Token: 0x04001EFA RID: 7930
		Ldind_I4,
		// Token: 0x04001EFB RID: 7931
		Ldind_U4,
		// Token: 0x04001EFC RID: 7932
		Ldind_I8,
		// Token: 0x04001EFD RID: 7933
		Ldind_I,
		// Token: 0x04001EFE RID: 7934
		Ldind_R4,
		// Token: 0x04001EFF RID: 7935
		Ldind_R8,
		// Token: 0x04001F00 RID: 7936
		Ldind_Ref,
		// Token: 0x04001F01 RID: 7937
		Stind_Ref,
		// Token: 0x04001F02 RID: 7938
		Stind_I1,
		// Token: 0x04001F03 RID: 7939
		Stind_I2,
		// Token: 0x04001F04 RID: 7940
		Stind_I4,
		// Token: 0x04001F05 RID: 7941
		Stind_I8,
		// Token: 0x04001F06 RID: 7942
		Stind_R4,
		// Token: 0x04001F07 RID: 7943
		Stind_R8,
		// Token: 0x04001F08 RID: 7944
		Add,
		// Token: 0x04001F09 RID: 7945
		Sub,
		// Token: 0x04001F0A RID: 7946
		Mul,
		// Token: 0x04001F0B RID: 7947
		Div,
		// Token: 0x04001F0C RID: 7948
		Div_Un,
		// Token: 0x04001F0D RID: 7949
		Rem,
		// Token: 0x04001F0E RID: 7950
		Rem_Un,
		// Token: 0x04001F0F RID: 7951
		And,
		// Token: 0x04001F10 RID: 7952
		Or,
		// Token: 0x04001F11 RID: 7953
		Xor,
		// Token: 0x04001F12 RID: 7954
		Shl,
		// Token: 0x04001F13 RID: 7955
		Shr,
		// Token: 0x04001F14 RID: 7956
		Shr_Un,
		// Token: 0x04001F15 RID: 7957
		Neg,
		// Token: 0x04001F16 RID: 7958
		Not,
		// Token: 0x04001F17 RID: 7959
		Conv_I1,
		// Token: 0x04001F18 RID: 7960
		Conv_I2,
		// Token: 0x04001F19 RID: 7961
		Conv_I4,
		// Token: 0x04001F1A RID: 7962
		Conv_I8,
		// Token: 0x04001F1B RID: 7963
		Conv_R4,
		// Token: 0x04001F1C RID: 7964
		Conv_R8,
		// Token: 0x04001F1D RID: 7965
		Conv_U4,
		// Token: 0x04001F1E RID: 7966
		Conv_U8,
		// Token: 0x04001F1F RID: 7967
		Callvirt,
		// Token: 0x04001F20 RID: 7968
		Cpobj,
		// Token: 0x04001F21 RID: 7969
		Ldobj,
		// Token: 0x04001F22 RID: 7970
		Ldstr,
		// Token: 0x04001F23 RID: 7971
		Newobj,
		// Token: 0x04001F24 RID: 7972
		Castclass,
		// Token: 0x04001F25 RID: 7973
		Isinst,
		// Token: 0x04001F26 RID: 7974
		Conv_R_Un,
		// Token: 0x04001F27 RID: 7975
		Unbox = 121,
		// Token: 0x04001F28 RID: 7976
		Throw,
		// Token: 0x04001F29 RID: 7977
		Ldfld,
		// Token: 0x04001F2A RID: 7978
		Ldflda,
		// Token: 0x04001F2B RID: 7979
		Stfld,
		// Token: 0x04001F2C RID: 7980
		Ldsfld,
		// Token: 0x04001F2D RID: 7981
		Ldsflda,
		// Token: 0x04001F2E RID: 7982
		Stsfld,
		// Token: 0x04001F2F RID: 7983
		Stobj,
		// Token: 0x04001F30 RID: 7984
		Conv_Ovf_I1_Un,
		// Token: 0x04001F31 RID: 7985
		Conv_Ovf_I2_Un,
		// Token: 0x04001F32 RID: 7986
		Conv_Ovf_I4_Un,
		// Token: 0x04001F33 RID: 7987
		Conv_Ovf_I8_Un,
		// Token: 0x04001F34 RID: 7988
		Conv_Ovf_U1_Un,
		// Token: 0x04001F35 RID: 7989
		Conv_Ovf_U2_Un,
		// Token: 0x04001F36 RID: 7990
		Conv_Ovf_U4_Un,
		// Token: 0x04001F37 RID: 7991
		Conv_Ovf_U8_Un,
		// Token: 0x04001F38 RID: 7992
		Conv_Ovf_I_Un,
		// Token: 0x04001F39 RID: 7993
		Conv_Ovf_U_Un,
		// Token: 0x04001F3A RID: 7994
		Box,
		// Token: 0x04001F3B RID: 7995
		Newarr,
		// Token: 0x04001F3C RID: 7996
		Ldlen,
		// Token: 0x04001F3D RID: 7997
		Ldelema,
		// Token: 0x04001F3E RID: 7998
		Ldelem_I1,
		// Token: 0x04001F3F RID: 7999
		Ldelem_U1,
		// Token: 0x04001F40 RID: 8000
		Ldelem_I2,
		// Token: 0x04001F41 RID: 8001
		Ldelem_U2,
		// Token: 0x04001F42 RID: 8002
		Ldelem_I4,
		// Token: 0x04001F43 RID: 8003
		Ldelem_U4,
		// Token: 0x04001F44 RID: 8004
		Ldelem_I8,
		// Token: 0x04001F45 RID: 8005
		Ldelem_I,
		// Token: 0x04001F46 RID: 8006
		Ldelem_R4,
		// Token: 0x04001F47 RID: 8007
		Ldelem_R8,
		// Token: 0x04001F48 RID: 8008
		Ldelem_Ref,
		// Token: 0x04001F49 RID: 8009
		Stelem_I,
		// Token: 0x04001F4A RID: 8010
		Stelem_I1,
		// Token: 0x04001F4B RID: 8011
		Stelem_I2,
		// Token: 0x04001F4C RID: 8012
		Stelem_I4,
		// Token: 0x04001F4D RID: 8013
		Stelem_I8,
		// Token: 0x04001F4E RID: 8014
		Stelem_R4,
		// Token: 0x04001F4F RID: 8015
		Stelem_R8,
		// Token: 0x04001F50 RID: 8016
		Stelem_Ref,
		// Token: 0x04001F51 RID: 8017
		Ldelem,
		// Token: 0x04001F52 RID: 8018
		Stelem,
		// Token: 0x04001F53 RID: 8019
		Unbox_Any,
		// Token: 0x04001F54 RID: 8020
		Conv_Ovf_I1 = 179,
		// Token: 0x04001F55 RID: 8021
		Conv_Ovf_U1,
		// Token: 0x04001F56 RID: 8022
		Conv_Ovf_I2,
		// Token: 0x04001F57 RID: 8023
		Conv_Ovf_U2,
		// Token: 0x04001F58 RID: 8024
		Conv_Ovf_I4,
		// Token: 0x04001F59 RID: 8025
		Conv_Ovf_U4,
		// Token: 0x04001F5A RID: 8026
		Conv_Ovf_I8,
		// Token: 0x04001F5B RID: 8027
		Conv_Ovf_U8,
		// Token: 0x04001F5C RID: 8028
		Refanyval = 194,
		// Token: 0x04001F5D RID: 8029
		Ckfinite,
		// Token: 0x04001F5E RID: 8030
		Mkrefany = 198,
		// Token: 0x04001F5F RID: 8031
		Ldtoken = 208,
		// Token: 0x04001F60 RID: 8032
		Conv_U2,
		// Token: 0x04001F61 RID: 8033
		Conv_U1,
		// Token: 0x04001F62 RID: 8034
		Conv_I,
		// Token: 0x04001F63 RID: 8035
		Conv_Ovf_I,
		// Token: 0x04001F64 RID: 8036
		Conv_Ovf_U,
		// Token: 0x04001F65 RID: 8037
		Add_Ovf,
		// Token: 0x04001F66 RID: 8038
		Add_Ovf_Un,
		// Token: 0x04001F67 RID: 8039
		Mul_Ovf,
		// Token: 0x04001F68 RID: 8040
		Mul_Ovf_Un,
		// Token: 0x04001F69 RID: 8041
		Sub_Ovf,
		// Token: 0x04001F6A RID: 8042
		Sub_Ovf_Un,
		// Token: 0x04001F6B RID: 8043
		Endfinally,
		// Token: 0x04001F6C RID: 8044
		Leave,
		// Token: 0x04001F6D RID: 8045
		Leave_S,
		// Token: 0x04001F6E RID: 8046
		Stind_I,
		// Token: 0x04001F6F RID: 8047
		Conv_U,
		// Token: 0x04001F70 RID: 8048
		Prefix7 = 248,
		// Token: 0x04001F71 RID: 8049
		Prefix6,
		// Token: 0x04001F72 RID: 8050
		Prefix5,
		// Token: 0x04001F73 RID: 8051
		Prefix4,
		// Token: 0x04001F74 RID: 8052
		Prefix3,
		// Token: 0x04001F75 RID: 8053
		Prefix2,
		// Token: 0x04001F76 RID: 8054
		Prefix1,
		// Token: 0x04001F77 RID: 8055
		Prefixref,
		// Token: 0x04001F78 RID: 8056
		Arglist = 65024,
		// Token: 0x04001F79 RID: 8057
		Ceq,
		// Token: 0x04001F7A RID: 8058
		Cgt,
		// Token: 0x04001F7B RID: 8059
		Cgt_Un,
		// Token: 0x04001F7C RID: 8060
		Clt,
		// Token: 0x04001F7D RID: 8061
		Clt_Un,
		// Token: 0x04001F7E RID: 8062
		Ldftn,
		// Token: 0x04001F7F RID: 8063
		Ldvirtftn,
		// Token: 0x04001F80 RID: 8064
		Ldarg = 65033,
		// Token: 0x04001F81 RID: 8065
		Ldarga,
		// Token: 0x04001F82 RID: 8066
		Starg,
		// Token: 0x04001F83 RID: 8067
		Ldloc,
		// Token: 0x04001F84 RID: 8068
		Ldloca,
		// Token: 0x04001F85 RID: 8069
		Stloc,
		// Token: 0x04001F86 RID: 8070
		Localloc,
		// Token: 0x04001F87 RID: 8071
		Endfilter = 65041,
		// Token: 0x04001F88 RID: 8072
		Unaligned_,
		// Token: 0x04001F89 RID: 8073
		Volatile_,
		// Token: 0x04001F8A RID: 8074
		Tail_,
		// Token: 0x04001F8B RID: 8075
		Initobj,
		// Token: 0x04001F8C RID: 8076
		Constrained_,
		// Token: 0x04001F8D RID: 8077
		Cpblk,
		// Token: 0x04001F8E RID: 8078
		Initblk,
		// Token: 0x04001F8F RID: 8079
		Rethrow = 65050,
		// Token: 0x04001F90 RID: 8080
		Sizeof = 65052,
		// Token: 0x04001F91 RID: 8081
		Refanytype,
		// Token: 0x04001F92 RID: 8082
		Readonly_
	}
}
