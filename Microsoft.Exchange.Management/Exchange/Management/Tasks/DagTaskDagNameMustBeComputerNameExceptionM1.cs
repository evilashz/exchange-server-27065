using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106F RID: 4207
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskDagNameMustBeComputerNameExceptionM1 : LocalizedException
	{
		// Token: 0x0600B10D RID: 45325 RVA: 0x00297616 File Offset: 0x00295816
		public DagTaskDagNameMustBeComputerNameExceptionM1(string dagName) : base(Strings.DagTaskDagNameMustBeComputerNameExceptionM1(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B10E RID: 45326 RVA: 0x0029762B File Offset: 0x0029582B
		public DagTaskDagNameMustBeComputerNameExceptionM1(string dagName, Exception innerException) : base(Strings.DagTaskDagNameMustBeComputerNameExceptionM1(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B10F RID: 45327 RVA: 0x00297641 File Offset: 0x00295841
		protected DagTaskDagNameMustBeComputerNameExceptionM1(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B110 RID: 45328 RVA: 0x0029766B File Offset: 0x0029586B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x1700386E RID: 14446
		// (get) Token: 0x0600B111 RID: 45329 RVA: 0x00297686 File Offset: 0x00295886
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061D4 RID: 25044
		private readonly string dagName;
	}
}
