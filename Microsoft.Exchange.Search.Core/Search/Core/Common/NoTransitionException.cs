using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	internal class NoTransitionException : InvalidOperationException
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002468 File Offset: 0x00000668
		public NoTransitionException(StatefulComponent component, uint state, uint signal) : base(string.Format("The transition for state {0} ({1}) and signal {2} ({3}) is not defined for component {4}", new object[]
		{
			state,
			component.GetStateName(state),
			signal,
			component.GetSignalName(signal),
			component
		}))
		{
			this.componentString = component.ToString();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024C3 File Offset: 0x000006C3
		protected NoTransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentString = (string)info.GetValue("ComponentString", typeof(string));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024ED File Offset: 0x000006ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ComponentString", this.componentString);
		}

		// Token: 0x0400000E RID: 14
		private const string ComponentStringLabel = "ComponentString";

		// Token: 0x0400000F RID: 15
		private string componentString;
	}
}
