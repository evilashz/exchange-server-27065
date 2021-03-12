using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class OwaEventNamespaceAttribute : Attribute
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x0005C7B7 File Offset: 0x0005A9B7
		public OwaEventNamespaceAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.eventInfoTable = new Hashtable();
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0005C7DF File Offset: 0x0005A9DF
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0005C7E7 File Offset: 0x0005A9E7
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0005C7EF File Offset: 0x0005A9EF
		internal ulong SegmentationFlags
		{
			get
			{
				return this.segmentationFlags;
			}
			set
			{
				this.segmentationFlags = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005C7F8 File Offset: 0x0005A9F8
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0005C800 File Offset: 0x0005AA00
		internal Type HandlerType
		{
			get
			{
				return this.handlerType;
			}
			set
			{
				this.handlerType = value;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0005C80C File Offset: 0x0005AA0C
		internal void AddEventInfo(OwaEventAttribute eventInfo)
		{
			if (!this.eventInfoTable.ContainsKey(eventInfo.Name))
			{
				this.eventInfoTable[eventInfo.Name] = eventInfo;
				return;
			}
			if (!eventInfo.IsAsync)
			{
				throw new OwaNotSupportedException(string.Format("Event name already exists in the namespace. '{0}'", eventInfo.Name));
			}
			OwaEventAttribute owaEventAttribute = (OwaEventAttribute)this.eventInfoTable[eventInfo.Name];
			if (!owaEventAttribute.IsAsync)
			{
				throw new OwaNotSupportedException(string.Format("Event name already exists in the namespace. '{0}'", eventInfo.Name));
			}
			if (eventInfo.BeginMethodInfo == null && eventInfo.EndMethodInfo != null && owaEventAttribute.BeginMethodInfo != null && owaEventAttribute.EndMethodInfo == null)
			{
				owaEventAttribute.EndMethodInfo = eventInfo.EndMethodInfo;
				return;
			}
			if (eventInfo.BeginMethodInfo != null && eventInfo.EndMethodInfo == null && owaEventAttribute.BeginMethodInfo == null && owaEventAttribute.EndMethodInfo != null)
			{
				this.eventInfoTable[eventInfo.Name] = eventInfo;
				eventInfo.EndMethodInfo = owaEventAttribute.EndMethodInfo;
				return;
			}
			throw new OwaNotSupportedException("Error registering async event.");
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005C941 File Offset: 0x0005AB41
		internal OwaEventAttribute FindEventInfo(string name)
		{
			return (OwaEventAttribute)this.eventInfoTable[name];
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0005C954 File Offset: 0x0005AB54
		internal Hashtable EventInfoTable
		{
			get
			{
				return this.eventInfoTable;
			}
		}

		// Token: 0x040009EE RID: 2542
		private string name;

		// Token: 0x040009EF RID: 2543
		private Hashtable eventInfoTable;

		// Token: 0x040009F0 RID: 2544
		private Type handlerType;

		// Token: 0x040009F1 RID: 2545
		private ulong segmentationFlags;
	}
}
