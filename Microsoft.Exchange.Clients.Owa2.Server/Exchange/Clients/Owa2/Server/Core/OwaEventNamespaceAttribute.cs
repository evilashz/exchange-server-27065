using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001DC RID: 476
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class OwaEventNamespaceAttribute : Attribute
	{
		// Token: 0x060010DC RID: 4316 RVA: 0x00040377 File Offset: 0x0003E577
		public OwaEventNamespaceAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.eventInfoTable = new Hashtable();
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0004039F File Offset: 0x0003E59F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000403A7 File Offset: 0x0003E5A7
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000403AF File Offset: 0x0003E5AF
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000403B8 File Offset: 0x0003E5B8
		internal Hashtable EventInfoTable
		{
			get
			{
				return this.eventInfoTable;
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000403C0 File Offset: 0x0003E5C0
		internal void AddEventInfo(OwaEventAttribute eventInfo)
		{
			if (!this.eventInfoTable.ContainsKey(eventInfo.Name))
			{
				this.eventInfoTable[eventInfo.Name] = eventInfo;
				return;
			}
			if (!eventInfo.IsAsync)
			{
				throw new OwaException(string.Format("Event name already exists in the namespace. '{0}'", eventInfo.Name));
			}
			OwaEventAttribute owaEventAttribute = (OwaEventAttribute)this.eventInfoTable[eventInfo.Name];
			if (!owaEventAttribute.IsAsync)
			{
				throw new OwaException(string.Format("Event name already exists in the namespace. '{0}'", eventInfo.Name));
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
			throw new OwaException("Error registering async event.");
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000404F5 File Offset: 0x0003E6F5
		internal OwaEventAttribute FindEventInfo(string name)
		{
			return (OwaEventAttribute)this.eventInfoTable[name];
		}

		// Token: 0x040009F6 RID: 2550
		private string name;

		// Token: 0x040009F7 RID: 2551
		private Hashtable eventInfoTable;

		// Token: 0x040009F8 RID: 2552
		private Type handlerType;
	}
}
