using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BB RID: 4539
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HuntGroupAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600B890 RID: 47248 RVA: 0x002A4C1B File Offset: 0x002A2E1B
		public HuntGroupAlreadyExistsException(string ipGateway, string pilotIdentifier) : base(Strings.ExceptionHuntGroupAlreadyExists(ipGateway, pilotIdentifier))
		{
			this.ipGateway = ipGateway;
			this.pilotIdentifier = pilotIdentifier;
		}

		// Token: 0x0600B891 RID: 47249 RVA: 0x002A4C38 File Offset: 0x002A2E38
		public HuntGroupAlreadyExistsException(string ipGateway, string pilotIdentifier, Exception innerException) : base(Strings.ExceptionHuntGroupAlreadyExists(ipGateway, pilotIdentifier), innerException)
		{
			this.ipGateway = ipGateway;
			this.pilotIdentifier = pilotIdentifier;
		}

		// Token: 0x0600B892 RID: 47250 RVA: 0x002A4C58 File Offset: 0x002A2E58
		protected HuntGroupAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipGateway = (string)info.GetValue("ipGateway", typeof(string));
			this.pilotIdentifier = (string)info.GetValue("pilotIdentifier", typeof(string));
		}

		// Token: 0x0600B893 RID: 47251 RVA: 0x002A4CAD File Offset: 0x002A2EAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipGateway", this.ipGateway);
			info.AddValue("pilotIdentifier", this.pilotIdentifier);
		}

		// Token: 0x17003A21 RID: 14881
		// (get) Token: 0x0600B894 RID: 47252 RVA: 0x002A4CD9 File Offset: 0x002A2ED9
		public string IpGateway
		{
			get
			{
				return this.ipGateway;
			}
		}

		// Token: 0x17003A22 RID: 14882
		// (get) Token: 0x0600B895 RID: 47253 RVA: 0x002A4CE1 File Offset: 0x002A2EE1
		public string PilotIdentifier
		{
			get
			{
				return this.pilotIdentifier;
			}
		}

		// Token: 0x0400643C RID: 25660
		private readonly string ipGateway;

		// Token: 0x0400643D RID: 25661
		private readonly string pilotIdentifier;
	}
}
