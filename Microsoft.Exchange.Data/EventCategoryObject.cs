using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class EventCategoryObject : ExEventCategory, IConfigurable, IComparable
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x0003A935 File Offset: 0x00038B35
		internal EventCategoryObject(string name, int number, ExEventLog.EventLevel level, EventCategoryIdentity id) : base(name, number, level)
		{
			this.m_identity = id;
			this.isValid = true;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0003A94F File Offset: 0x00038B4F
		public EventCategoryObject()
		{
			this.m_identity = null;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0003A95E File Offset: 0x00038B5E
		public override string ToString()
		{
			return string.Format("{0} {1}", base.Number, base.Name);
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0003A97B File Offset: 0x00038B7B
		public ObjectId Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0003A983 File Offset: 0x00038B83
		public ValidationError[] Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0003A98A File Offset: 0x00038B8A
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x0003A992 File Offset: 0x00038B92
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0003A99B File Offset: 0x00038B9B
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0003A99E File Offset: 0x00038B9E
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0003A9A5 File Offset: 0x00038BA5
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0003A9AC File Offset: 0x00038BAC
		public int CompareTo(object value)
		{
			EventCategoryObject eventCategoryObject = value as EventCategoryObject;
			if (eventCategoryObject != null)
			{
				return base.Name.CompareTo(eventCategoryObject.Name);
			}
			throw new ArgumentException("Object is not an EventCategoryObject");
		}

		// Token: 0x04000B46 RID: 2886
		private EventCategoryIdentity m_identity;

		// Token: 0x04000B47 RID: 2887
		private bool isValid;
	}
}
