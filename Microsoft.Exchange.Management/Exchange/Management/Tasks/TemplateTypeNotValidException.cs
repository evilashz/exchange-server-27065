using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F8 RID: 4344
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TemplateTypeNotValidException : LocalizedException
	{
		// Token: 0x0600B3C1 RID: 46017 RVA: 0x0029BA86 File Offset: 0x00299C86
		public TemplateTypeNotValidException(string type) : base(Strings.TemplateTypeNotValid(type))
		{
			this.type = type;
		}

		// Token: 0x0600B3C2 RID: 46018 RVA: 0x0029BA9B File Offset: 0x00299C9B
		public TemplateTypeNotValidException(string type, Exception innerException) : base(Strings.TemplateTypeNotValid(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x0600B3C3 RID: 46019 RVA: 0x0029BAB1 File Offset: 0x00299CB1
		protected TemplateTypeNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600B3C4 RID: 46020 RVA: 0x0029BADB File Offset: 0x00299CDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x170038FE RID: 14590
		// (get) Token: 0x0600B3C5 RID: 46021 RVA: 0x0029BAF6 File Offset: 0x00299CF6
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04006264 RID: 25188
		private readonly string type;
	}
}
