using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC3 RID: 4035
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetTransportServerPropertyOnSubscribedEdgeException : LocalizedException
	{
		// Token: 0x0600ADA6 RID: 44454 RVA: 0x00292055 File Offset: 0x00290255
		public CannotSetTransportServerPropertyOnSubscribedEdgeException(string propertyName) : base(Strings.ErrorCannotSetTransportServerPropertyOnSubscribedEdge(propertyName))
		{
			this.propertyName = propertyName;
		}

		// Token: 0x0600ADA7 RID: 44455 RVA: 0x0029206A File Offset: 0x0029026A
		public CannotSetTransportServerPropertyOnSubscribedEdgeException(string propertyName, Exception innerException) : base(Strings.ErrorCannotSetTransportServerPropertyOnSubscribedEdge(propertyName), innerException)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x0600ADA8 RID: 44456 RVA: 0x00292080 File Offset: 0x00290280
		protected CannotSetTransportServerPropertyOnSubscribedEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x0600ADA9 RID: 44457 RVA: 0x002920AA File Offset: 0x002902AA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x170037B7 RID: 14263
		// (get) Token: 0x0600ADAA RID: 44458 RVA: 0x002920C5 File Offset: 0x002902C5
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0400611D RID: 24861
		private readonly string propertyName;
	}
}
