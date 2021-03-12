using System;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public abstract class Condition
	{
		// Token: 0x06000334 RID: 820
		public abstract bool Verify();

		// Token: 0x06000335 RID: 821 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		public void VerifyAndThrow(LocalizedString ls)
		{
			this.VerifyAndThrow(new LocalizedException(ls));
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000CA89 File Offset: 0x0000AC89
		public void VerifyAndThrow(LocalizedException le)
		{
			if (!this.Verify())
			{
				throw le;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public bool Match(Condition conditionToMatch)
		{
			TaskLogger.LogEnter(new object[]
			{
				this,
				conditionToMatch
			});
			if (conditionToMatch == null)
			{
				throw new ArgumentNullException("conditionToMatch");
			}
			bool result = false;
			if (base.GetType() != conditionToMatch.GetType())
			{
				TaskLogger.Trace(Strings.LogConditionMatchingTypeMismacth(base.GetType(), conditionToMatch.GetType()));
			}
			else
			{
				foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					object obj = propertyInfo.GetGetMethod().Invoke(this, null);
					object obj2 = propertyInfo.GetGetMethod().Invoke(conditionToMatch, null);
					TaskLogger.Trace("{0}.{1}: {2} ?=? {3}", new object[]
					{
						base.GetType().FullName,
						propertyInfo.Name,
						obj,
						obj2
					});
					if ((obj != null || obj2 != null) && (obj == null || !obj.Equals(obj2)))
					{
						TaskLogger.Trace(Strings.LogConditionMatchingPropertyMismatch(base.GetType(), propertyInfo.Name, obj, obj2));
						goto IL_106;
					}
				}
				result = true;
			}
			IL_106:
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetType().ToString());
			stringBuilder.Append("(");
			bool flag = true;
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				string name = propertyInfo.Name;
				object obj = propertyInfo.GetGetMethod().Invoke(this, null);
				stringBuilder.Append(name);
				stringBuilder.Append("=");
				stringBuilder.Append((obj == null) ? "null" : obj.ToString());
				flag = false;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		internal ConditionAttribute Role
		{
			get
			{
				ExAssert.RetailAssert(null != this.role, "The condition role was not yet set.");
				return this.role;
			}
			set
			{
				ExAssert.RetailAssert(null != value, "Cannot set the condition role to null.");
				ExAssert.RetailAssert(null == this.role, "The condition role is already set and cannot be changed.");
				this.role = value;
			}
		}

		// Token: 0x040000D9 RID: 217
		private ConditionAttribute role;
	}
}
