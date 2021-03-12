using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000101 RID: 257
	internal abstract class KeyMappingBase : IDataItem
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0001FB43 File Offset: 0x0001DD43
		internal KeyMappingBase(KeyMappingTypeEnum type, int k, string context)
		{
			this.keyMappingType = type;
			this.key = k;
			this.context = context;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001FB60 File Offset: 0x0001DD60
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0001FB68 File Offset: 0x0001DD68
		internal KeyMappingTypeEnum KeyMappingType
		{
			get
			{
				return this.keyMappingType;
			}
			set
			{
				this.keyMappingType = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001FB71 File Offset: 0x0001DD71
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0001FB79 File Offset: 0x0001DD79
		internal int Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001FB82 File Offset: 0x0001DD82
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001FB8A File Offset: 0x0001DD8A
		internal string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001FB93 File Offset: 0x0001DD93
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001FB9B File Offset: 0x0001DD9B
		internal PAAValidationResult ValidationResult
		{
			get
			{
				return this.validationResult;
			}
			set
			{
				this.validationResult = value;
			}
		}

		// Token: 0x0600083E RID: 2110
		public abstract bool Validate(IDataValidator validator);

		// Token: 0x0600083F RID: 2111 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		internal static TransferToNumber CreateTransferToNumber(int key, string context, string number)
		{
			return new TransferToNumber(key, context, number);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001FBAE File Offset: 0x0001DDAE
		internal static TransferToADContactMailbox CreateTransferToADContactMailbox(int key, string context, string legacyExchangeDN)
		{
			return new TransferToADContactMailbox(key, context, legacyExchangeDN);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		internal static TransferToADContactPhone CreateTransferToADContactPhone(int key, string context, string legacyExchangeDN)
		{
			return new TransferToADContactPhone(key, context, legacyExchangeDN);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
		internal static TransferToFindMe CreateFindMe(int key, string context, string number, int timeout)
		{
			return new TransferToFindMe(key, context, new FindMeNumbers(number, timeout));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001FBD2 File Offset: 0x0001DDD2
		internal static TransferToFindMe CreateFindMe(int key, string context, string number, int timeout, string label)
		{
			return new TransferToFindMe(key, context, new FindMeNumbers(number, timeout, label));
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001FBE4 File Offset: 0x0001DDE4
		internal static TransferToVoicemail CreateTransferToVoicemail(string context)
		{
			return new TransferToVoicemail(context);
		}

		// Token: 0x040004C9 RID: 1225
		private KeyMappingTypeEnum keyMappingType;

		// Token: 0x040004CA RID: 1226
		private string context;

		// Token: 0x040004CB RID: 1227
		private int key;

		// Token: 0x040004CC RID: 1228
		private PAAValidationResult validationResult;
	}
}
