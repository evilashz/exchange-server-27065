using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A3 RID: 163
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceNotReadyException : DxStoreInstanceServerException
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001460F File Offset: 0x0001280F
		public DxStoreInstanceNotReadyException(string currentState) : base(Strings.DxStoreInstanceNotReady(currentState))
		{
			this.currentState = currentState;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00014629 File Offset: 0x00012829
		public DxStoreInstanceNotReadyException(string currentState, Exception innerException) : base(Strings.DxStoreInstanceNotReady(currentState), innerException)
		{
			this.currentState = currentState;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00014644 File Offset: 0x00012844
		protected DxStoreInstanceNotReadyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.currentState = (string)info.GetValue("currentState", typeof(string));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001466E File Offset: 0x0001286E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("currentState", this.currentState);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00014689 File Offset: 0x00012889
		public string CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x04000292 RID: 658
		private readonly string currentState;
	}
}
