using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D3 RID: 4307
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TPDWithoutPrivateKeyException : LocalizedException
	{
		// Token: 0x0600B310 RID: 45840 RVA: 0x0029AAEE File Offset: 0x00298CEE
		public TPDWithoutPrivateKeyException(string name) : base(Strings.TPDWithoutPrivateKey(name))
		{
			this.name = name;
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x0029AB03 File Offset: 0x00298D03
		public TPDWithoutPrivateKeyException(string name, Exception innerException) : base(Strings.TPDWithoutPrivateKey(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B312 RID: 45842 RVA: 0x0029AB19 File Offset: 0x00298D19
		protected TPDWithoutPrivateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B313 RID: 45843 RVA: 0x0029AB43 File Offset: 0x00298D43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170038E1 RID: 14561
		// (get) Token: 0x0600B314 RID: 45844 RVA: 0x0029AB5E File Offset: 0x00298D5E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006247 RID: 25159
		private readonly string name;
	}
}
