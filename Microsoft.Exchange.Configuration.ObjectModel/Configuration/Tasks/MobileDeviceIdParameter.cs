using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000D7 RID: 215
	[Serializable]
	public class MobileDeviceIdParameter : ADIdParameter
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0001D29C File Offset: 0x0001B49C
		public MobileDeviceIdParameter()
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		public MobileDeviceIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		public MobileDeviceIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001D320 File Offset: 0x0001B520
		public MobileDeviceIdParameter(MobileDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.Initialize(device.Id);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001D364 File Offset: 0x0001B564
		public MobileDeviceIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001D38E File Offset: 0x0001B58E
		internal string DeviceId
		{
			get
			{
				if (this.deviceId == null)
				{
					this.ComputeIdAndDeviceTypeAndClientType();
				}
				return this.deviceId;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		internal string DeviceType
		{
			get
			{
				if (this.deviceType == null)
				{
					this.ComputeIdAndDeviceTypeAndClientType();
				}
				return this.deviceType;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001D3BA File Offset: 0x0001B5BA
		internal MobileClientType ClientType
		{
			get
			{
				if (this.clientType == null)
				{
					this.ComputeIdAndDeviceTypeAndClientType();
				}
				return this.clientType.Value;
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001D3DA File Offset: 0x0001B5DA
		public static MobileDeviceIdParameter Parse(string identity)
		{
			return new MobileDeviceIdParameter(identity);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (!typeof(MobileDevice).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			notFoundReason = new LocalizedString?(Strings.WrongActiveSyncDeviceIdParameter(this.ToString()));
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetExactMatchObjects<T>(rootId, subTreeSession, optionalData));
			if (!wrapper.HasElements() && base.RawIdentity != null)
			{
				string[] array = base.RawIdentity.Split(new char[]
				{
					'\\'
				});
				if (array.Length == 3)
				{
					string text = array[0];
					string text2 = array[2];
					if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
					{
						wrapper = EnumerableWrapper<T>.GetWrapper(this.GetObjectsInOrganization<T>(text2, rootId, session, optionalData), new MobileDeviceIdParameter.MobileDeviceUsernameFilter<T>(text));
					}
				}
			}
			return wrapper;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		internal MailboxIdParameter GetMailboxId()
		{
			ADObjectId internalADObjectId = base.InternalADObjectId;
			if (internalADObjectId == null)
			{
				ADIdParameter.TryResolveCanonicalName(base.RawIdentity, out internalADObjectId);
			}
			if (internalADObjectId == null || internalADObjectId.Parent == null || internalADObjectId.Parent.Parent == null)
			{
				return null;
			}
			return new MailboxIdParameter(internalADObjectId.Parent.Parent);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001D50C File Offset: 0x0001B70C
		private string ParseDeviceType(string part)
		{
			string text = "ExchangeActiveSyncDevices/";
			int num = part.IndexOf(text, StringComparison.OrdinalIgnoreCase);
			string result;
			if (0 <= num)
			{
				result = part.Substring(num + text.Length);
			}
			else
			{
				result = part;
			}
			return result;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001D540 File Offset: 0x0001B740
		private void ComputeIdAndDeviceTypeAndClientType()
		{
			string text = null;
			if (base.InternalADObjectId != null)
			{
				text = base.InternalADObjectId.Rdn.UnescapedName;
			}
			else if (!string.IsNullOrEmpty(base.RawIdentity))
			{
				int num = base.RawIdentity.LastIndexOf('\\');
				text = ((num != -1 && num < base.RawIdentity.Length - 1) ? base.RawIdentity.Substring(num + 1) : base.RawIdentity);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("Id is null or empty");
			}
			string[] array = text.Split(this.TypeIdSeparatorChars, 3);
			if (array.Length == 3)
			{
				this.deviceId = array[2];
				this.deviceType = this.ParseDeviceType(array[1]);
				this.clientType = new MobileClientType?(string.Equals("MOWA", array[0], StringComparison.OrdinalIgnoreCase) ? MobileClientType.MOWA : MobileClientType.EAS);
				return;
			}
			if (array.Length == 2)
			{
				this.deviceId = array[1];
				this.deviceType = this.ParseDeviceType(array[0]);
				this.clientType = new MobileClientType?(MobileClientType.EAS);
				return;
			}
			throw new ArgumentException("Id is invalid.");
		}

		// Token: 0x0400024A RID: 586
		public const char TypeIdSeparatorChar = '§';

		// Token: 0x0400024B RID: 587
		private readonly char[] TypeIdSeparatorChars = new char[]
		{
			'§'
		};

		// Token: 0x0400024C RID: 588
		private string deviceId;

		// Token: 0x0400024D RID: 589
		private string deviceType;

		// Token: 0x0400024E RID: 590
		private MobileClientType? clientType;

		// Token: 0x020000D8 RID: 216
		private class MobileDeviceUsernameFilter<T> : IEnumerableFilter<T> where T : IConfigurable
		{
			// Token: 0x060007F8 RID: 2040 RVA: 0x0001D644 File Offset: 0x0001B844
			public MobileDeviceUsernameFilter(string username)
			{
				if (username == null)
				{
					throw new ArgumentNullException("username", username);
				}
				this.username = username;
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0001D664 File Offset: 0x0001B864
			public bool AcceptElement(T element)
			{
				if (element == null)
				{
					return false;
				}
				ADObjectId adobjectId = element.Identity as ADObjectId;
				return adobjectId != null && adobjectId.Parent != null && adobjectId.Parent.Parent != null && string.Equals(adobjectId.Parent.Parent.Name, this.username, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0400024F RID: 591
			private readonly string username;
		}
	}
}
