using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E16 RID: 3606
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AceIsUnsupportedTypeException : LocalizedException
	{
		// Token: 0x0600A571 RID: 42353 RVA: 0x002860C9 File Offset: 0x002842C9
		public AceIsUnsupportedTypeException(string aceType) : base(Strings.AceIsUnsupportedTypeException(aceType))
		{
			this.aceType = aceType;
		}

		// Token: 0x0600A572 RID: 42354 RVA: 0x002860DE File Offset: 0x002842DE
		public AceIsUnsupportedTypeException(string aceType, Exception innerException) : base(Strings.AceIsUnsupportedTypeException(aceType), innerException)
		{
			this.aceType = aceType;
		}

		// Token: 0x0600A573 RID: 42355 RVA: 0x002860F4 File Offset: 0x002842F4
		protected AceIsUnsupportedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.aceType = (string)info.GetValue("aceType", typeof(string));
		}

		// Token: 0x0600A574 RID: 42356 RVA: 0x0028611E File Offset: 0x0028431E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("aceType", this.aceType);
		}

		// Token: 0x17003636 RID: 13878
		// (get) Token: 0x0600A575 RID: 42357 RVA: 0x00286139 File Offset: 0x00284339
		public string AceType
		{
			get
			{
				return this.aceType;
			}
		}

		// Token: 0x04005F9C RID: 24476
		private readonly string aceType;
	}
}
