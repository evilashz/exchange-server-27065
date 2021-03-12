using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class UnifiedPolicySyncNotificationIdParameter : IIdentityParameter
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00026F73 File Offset: 0x00025173
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00026F7B File Offset: 0x0002517B
		internal UnifiedPolicySyncNotificationId NotificatonId { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00026F84 File Offset: 0x00025184
		public string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00026F8C File Offset: 0x0002518C
		public UnifiedPolicySyncNotificationIdParameter()
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00026F94 File Offset: 0x00025194
		public UnifiedPolicySyncNotificationIdParameter(ConfigurableObject configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			((IIdentityParameter)this).Initialize(configurableObject.Identity);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00026FB6 File Offset: 0x000251B6
		public UnifiedPolicySyncNotificationIdParameter(string notificationIdValue)
		{
			if (string.IsNullOrEmpty(notificationIdValue))
			{
				throw new ArgumentNullException("notificationIdValue");
			}
			this.Initialize(new UnifiedPolicySyncNotificationId(notificationIdValue));
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00026FDD File Offset: 0x000251DD
		public void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is UnifiedPolicySyncNotificationId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.NotificatonId = (UnifiedPolicySyncNotificationId)objectId;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002701C File Offset: 0x0002521C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00027034 File Offset: 0x00025234
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.NotificatonId == null)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			IConfigurable[] array = session.Find<T>(null, this.NotificatonId, false, null);
			if (array == null || array.Length == 0)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			T[] array2 = new T[array.Length];
			int num = 0;
			foreach (IConfigurable configurable in array)
			{
				array2[num++] = (T)((object)configurable);
			}
			return array2;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000270DE File Offset: 0x000252DE
		public override string ToString()
		{
			if (this.NotificatonId == null)
			{
				return null;
			}
			return this.NotificatonId.IdValue;
		}
	}
}
