using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005CF RID: 1487
	[Serializable]
	internal struct MetadataToken
	{
		// Token: 0x0600455D RID: 17757 RVA: 0x000FDFAA File Offset: 0x000FC1AA
		public static implicit operator int(MetadataToken token)
		{
			return token.Value;
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000FDFB2 File Offset: 0x000FC1B2
		public static implicit operator MetadataToken(int token)
		{
			return new MetadataToken(token);
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000FDFBC File Offset: 0x000FC1BC
		public static bool IsTokenOfType(int token, params MetadataTokenType[] types)
		{
			for (int i = 0; i < types.Length; i++)
			{
				if ((MetadataTokenType)((long)token & (long)((ulong)-16777216)) == types[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x000FDFE9 File Offset: 0x000FC1E9
		public static bool IsNullToken(int token)
		{
			return (token & 16777215) == 0;
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x000FDFF5 File Offset: 0x000FC1F5
		public MetadataToken(int token)
		{
			this.Value = token;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x000FDFFE File Offset: 0x000FC1FE
		public bool IsGlobalTypeDefToken
		{
			get
			{
				return this.Value == 33554433;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x000FE00D File Offset: 0x000FC20D
		public MetadataTokenType TokenType
		{
			get
			{
				return (MetadataTokenType)((long)this.Value & (long)((ulong)-16777216));
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x000FE01E File Offset: 0x000FC21E
		public bool IsTypeRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeRef;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x000FE02D File Offset: 0x000FC22D
		public bool IsTypeDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeDef;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x000FE03C File Offset: 0x000FC23C
		public bool IsFieldDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.FieldDef;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x000FE04B File Offset: 0x000FC24B
		public bool IsMethodDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodDef;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x000FE05A File Offset: 0x000FC25A
		public bool IsMemberRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MemberRef;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x000FE069 File Offset: 0x000FC269
		public bool IsEvent
		{
			get
			{
				return this.TokenType == MetadataTokenType.Event;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x000FE078 File Offset: 0x000FC278
		public bool IsProperty
		{
			get
			{
				return this.TokenType == MetadataTokenType.Property;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x000FE087 File Offset: 0x000FC287
		public bool IsParamDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.ParamDef;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000FE096 File Offset: 0x000FC296
		public bool IsTypeSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeSpec;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x000FE0A5 File Offset: 0x000FC2A5
		public bool IsMethodSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodSpec;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000FE0B4 File Offset: 0x000FC2B4
		public bool IsString
		{
			get
			{
				return this.TokenType == MetadataTokenType.String;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x000FE0C3 File Offset: 0x000FC2C3
		public bool IsSignature
		{
			get
			{
				return this.TokenType == MetadataTokenType.Signature;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x000FE0D2 File Offset: 0x000FC2D2
		public bool IsModule
		{
			get
			{
				return this.TokenType == MetadataTokenType.Module;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x000FE0DD File Offset: 0x000FC2DD
		public bool IsAssembly
		{
			get
			{
				return this.TokenType == MetadataTokenType.Assembly;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x000FE0EC File Offset: 0x000FC2EC
		public bool IsGenericPar
		{
			get
			{
				return this.TokenType == MetadataTokenType.GenericPar;
			}
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x000FE0FB File Offset: 0x000FC2FB
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x8}", this.Value);
		}

		// Token: 0x04001C94 RID: 7316
		public int Value;
	}
}
