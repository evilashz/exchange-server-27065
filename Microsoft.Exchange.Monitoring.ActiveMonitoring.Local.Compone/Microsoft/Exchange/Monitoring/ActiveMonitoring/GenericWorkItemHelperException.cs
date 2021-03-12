using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class GenericWorkItemHelperException : Exception
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0001E256 File Offset: 0x0001C456
		public GenericWorkItemHelperException()
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001E25E File Offset: 0x0001C45E
		public GenericWorkItemHelperException(string message) : base(message)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001E267 File Offset: 0x0001C467
		public GenericWorkItemHelperException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001E271 File Offset: 0x0001C471
		protected GenericWorkItemHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
