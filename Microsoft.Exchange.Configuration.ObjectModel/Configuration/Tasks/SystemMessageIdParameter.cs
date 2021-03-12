using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class SystemMessageIdParameter : ADIdParameter
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x00020A1C File Offset: 0x0001EC1C
		public SystemMessageIdParameter()
		{
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00020A24 File Offset: 0x0001EC24
		public SystemMessageIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00020A2D File Offset: 0x0001EC2D
		public SystemMessageIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00020A38 File Offset: 0x0001EC38
		protected SystemMessageIdParameter(string identity) : base(identity)
		{
			string[] array = identity.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 1)
			{
				if (base.InternalADObjectId == null)
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
				}
				return;
			}
			else if (array.Length == 2)
			{
				CultureInfo cultureInfo;
				if (string.IsNullOrEmpty(array[0]) || string.IsNullOrEmpty(array[1]) || !this.TryGetCulture(array[0], out cultureInfo) || !this.IsValidQuotaMessageType(array[1]))
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
				}
				this.relativeDnParts = new string[2];
				this.relativeDnParts[0] = cultureInfo.LCID.ToString(NumberFormatInfo.InvariantInfo);
				this.relativeDnParts[1] = array[1];
				return;
			}
			else
			{
				if (array.Length != 3)
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
				}
				CultureInfo cultureInfo2;
				if (string.IsNullOrEmpty(array[0]) || string.IsNullOrEmpty(array[1]) || string.IsNullOrEmpty(array[2]) || !this.TryGetCulture(array[0], out cultureInfo2) || (!array[1].Equals("internal", StringComparison.OrdinalIgnoreCase) && !array[1].Equals("external", StringComparison.OrdinalIgnoreCase)) || !EnhancedStatusCode.IsValid(array[2]))
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
				}
				this.relativeDnParts = new string[3];
				this.relativeDnParts[0] = cultureInfo2.LCID.ToString(NumberFormatInfo.InvariantInfo);
				this.relativeDnParts[1] = array[1];
				this.relativeDnParts[2] = array[2];
				return;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00020BC9 File Offset: 0x0001EDC9
		public static SystemMessageIdParameter Parse(string identity)
		{
			return new SystemMessageIdParameter(identity);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			notFoundReason = null;
			List<T> list = new List<T>();
			try
			{
				if (typeof(T) != typeof(SystemMessage))
				{
					throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
				}
				if (session == null)
				{
					throw new ArgumentNullException("session");
				}
				if (string.IsNullOrEmpty(base.RawIdentity))
				{
					throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
				}
				if (base.InternalADObjectId != null)
				{
					return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
				}
				ADObjectId adobjectId = rootId;
				foreach (string unescapedCommonName in this.relativeDnParts)
				{
					adobjectId = adobjectId.GetChildId(unescapedCommonName);
				}
				if (optionalData != null && optionalData.AdditionalFilter != null)
				{
					throw new NotSupportedException("Supplying Additional Filters without an ADObjectId is not currently supported by this IdParameter.");
				}
				IConfigurable configurable = ((IConfigDataProvider)session).Read<T>(adobjectId);
				if (configurable != null)
				{
					list.Add((T)((object)configurable));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return list;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		private bool TryGetCulture(string culture, out CultureInfo cultureInfo)
		{
			cultureInfo = null;
			try
			{
				cultureInfo = CultureInfo.GetCultureInfo(culture);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00020D28 File Offset: 0x0001EF28
		private bool IsValidQuotaMessageType(string messageType)
		{
			try
			{
				Enum.Parse(typeof(QuotaMessageType), messageType, true);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000273 RID: 627
		private const char CommonNameSeperatorChar = '\\';

		// Token: 0x04000274 RID: 628
		private string[] relativeDnParts;
	}
}
