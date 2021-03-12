using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E7 RID: 231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmGetFqdnFailedADErrorException : AmServerNameResolveFqdnException
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0001CB01 File Offset: 0x0001AD01
		public AmGetFqdnFailedADErrorException(string nodeName, string adError) : base(Strings.AmGetFqdnFailedADError(nodeName, adError))
		{
			this.nodeName = nodeName;
			this.adError = adError;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001CB23 File Offset: 0x0001AD23
		public AmGetFqdnFailedADErrorException(string nodeName, string adError, Exception innerException) : base(Strings.AmGetFqdnFailedADError(nodeName, adError), innerException)
		{
			this.nodeName = nodeName;
			this.adError = adError;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001CB48 File Offset: 0x0001AD48
		protected AmGetFqdnFailedADErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.adError = (string)info.GetValue("adError", typeof(string));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001CB9D File Offset: 0x0001AD9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("adError", this.adError);
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001CBC9 File Offset: 0x0001ADC9
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001CBD1 File Offset: 0x0001ADD1
		public string AdError
		{
			get
			{
				return this.adError;
			}
		}

		// Token: 0x04000742 RID: 1858
		private readonly string nodeName;

		// Token: 0x04000743 RID: 1859
		private readonly string adError;
	}
}
