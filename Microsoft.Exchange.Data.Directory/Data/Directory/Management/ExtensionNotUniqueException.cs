using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000ACD RID: 2765
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExtensionNotUniqueException : LocalizedException
	{
		// Token: 0x060080C4 RID: 32964 RVA: 0x001A5D41 File Offset: 0x001A3F41
		public ExtensionNotUniqueException(string s, string dialPlan) : base(DirectoryStrings.ExtensionNotUnique(s, dialPlan))
		{
			this.s = s;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x001A5D5E File Offset: 0x001A3F5E
		public ExtensionNotUniqueException(string s, string dialPlan, Exception innerException) : base(DirectoryStrings.ExtensionNotUnique(s, dialPlan), innerException)
		{
			this.s = s;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080C6 RID: 32966 RVA: 0x001A5D7C File Offset: 0x001A3F7C
		protected ExtensionNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
			this.dialPlan = (string)info.GetValue("dialPlan", typeof(string));
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x001A5DD1 File Offset: 0x001A3FD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
			info.AddValue("dialPlan", this.dialPlan);
		}

		// Token: 0x17002EE7 RID: 12007
		// (get) Token: 0x060080C8 RID: 32968 RVA: 0x001A5DFD File Offset: 0x001A3FFD
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x17002EE8 RID: 12008
		// (get) Token: 0x060080C9 RID: 32969 RVA: 0x001A5E05 File Offset: 0x001A4005
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x040055C1 RID: 21953
		private readonly string s;

		// Token: 0x040055C2 RID: 21954
		private readonly string dialPlan;
	}
}
