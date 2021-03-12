using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200018A RID: 394
	internal abstract class OwaEventParserBase
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x0005C02F File Offset: 0x0005A22F
		internal OwaEventParserBase(OwaEventHandlerBase eventHandler, int parameterTableCapacity)
		{
			this.eventHandler = eventHandler;
			this.parameterTable = new Hashtable(parameterTableCapacity);
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0005C04A File Offset: 0x0005A24A
		protected Hashtable ParameterTable
		{
			get
			{
				return this.parameterTable;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0005C052 File Offset: 0x0005A252
		protected ulong SetMask
		{
			get
			{
				return this.setMask;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005C05A File Offset: 0x0005A25A
		protected OwaEventHandlerBase EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0005C064 File Offset: 0x0005A264
		internal Hashtable Parse()
		{
			Hashtable result = this.ParseParameters();
			if ((this.EventHandler.EventInfo.RequiredMask & this.SetMask) != this.EventHandler.EventInfo.RequiredMask)
			{
				this.ThrowParserException("A required parameter of the event wasn't set");
			}
			return result;
		}

		// Token: 0x06000E73 RID: 3699
		protected abstract Hashtable ParseParameters();

		// Token: 0x06000E74 RID: 3700
		protected abstract void ThrowParserException(string description);

		// Token: 0x06000E75 RID: 3701 RVA: 0x0005C0AD File Offset: 0x0005A2AD
		protected void ThrowParserException()
		{
			this.ThrowParserException(null);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0005C0B8 File Offset: 0x0005A2B8
		protected OwaEventParameterAttribute GetParamInfo(string name)
		{
			OwaEventParameterAttribute owaEventParameterAttribute = this.EventHandler.EventInfo.FindParameterInfo(name);
			if (owaEventParameterAttribute == null)
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' is unknown.", new object[]
				{
					name
				}));
			}
			return owaEventParameterAttribute;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0005C0FC File Offset: 0x0005A2FC
		protected void AddParameter(OwaEventParameterAttribute paramInfo, object value)
		{
			if (this.parameterTable.Count >= 64)
			{
				this.ThrowParserException("Reached maximum number of parameters");
			}
			if ((this.setMask & paramInfo.ParameterMask) != 0UL)
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' is found twice", new object[]
				{
					paramInfo.Name
				}));
			}
			this.setMask |= paramInfo.ParameterMask;
			this.parameterTable.Add(paramInfo.Name, value);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0005C180 File Offset: 0x0005A380
		protected void AddSimpleTypeParameter(OwaEventParameterAttribute paramInfo, string value)
		{
			object value2;
			if (paramInfo.Type == typeof(string))
			{
				value2 = value;
			}
			else
			{
				value2 = this.ConvertToStrongType(paramInfo.Type, value);
			}
			this.AddParameter(paramInfo, value2);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005C1C0 File Offset: 0x0005A3C0
		protected void AddStructParameter(OwaEventParameterAttribute paramInfo, object value)
		{
			this.AddParameter(paramInfo, value);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0005C1CC File Offset: 0x0005A3CC
		protected void AddArrayParameter(OwaEventParameterAttribute paramInfo, ArrayList itemArray)
		{
			Array value = itemArray.ToArray(paramInfo.Type);
			this.AddParameter(paramInfo, value);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005C1EE File Offset: 0x0005A3EE
		protected void AddEmptyParameter(OwaEventParameterAttribute paramInfo)
		{
			if (paramInfo.IsStruct && !paramInfo.IsArray)
			{
				this.ThrowParserException("Empty structs not supported");
				return;
			}
			if (paramInfo.IsArray)
			{
				this.AddArrayParameter(paramInfo, new ArrayList());
				return;
			}
			this.AddSimpleTypeParameter(paramInfo, string.Empty);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0005C22D File Offset: 0x0005A42D
		private void AddItemToArray(ArrayList itemArray, object value)
		{
			if (itemArray.Count >= 2000)
			{
				this.ThrowParserException("Reached maximum number of items in an array");
			}
			itemArray.Add(value);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0005C250 File Offset: 0x0005A450
		protected void AddSimpleTypeToArray(OwaEventParameterAttribute paramInfo, ArrayList itemArray, string value)
		{
			object value2;
			if (paramInfo.Type == typeof(string))
			{
				value2 = value;
			}
			else
			{
				value2 = this.ConvertToStrongType(paramInfo.Type, value);
			}
			this.AddItemToArray(itemArray, value2);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0005C290 File Offset: 0x0005A490
		protected void AddStructToArray(OwaEventParameterAttribute paramInfo, ArrayList itemArray, object value)
		{
			this.AddItemToArray(itemArray, value);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005C29A File Offset: 0x0005A49A
		protected void AddEmptyItemToArray(OwaEventParameterAttribute paramInfo, ArrayList itemArray)
		{
			if (paramInfo.IsStruct)
			{
				this.ThrowParserException("Empty structs not supported");
				return;
			}
			this.AddSimpleTypeParameter(paramInfo, string.Empty);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0005C2BC File Offset: 0x0005A4BC
		protected object ConvertToStrongType(Type paramType, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
				{
					paramType,
					value
				}));
			}
			try
			{
				if (paramType.IsEnum)
				{
					OwaEventEnumAttribute owaEventEnumAttribute = OwaEventRegistry.FindEnumInfo(paramType);
					int intValue = int.Parse(value, CultureInfo.InvariantCulture);
					object obj = owaEventEnumAttribute.FindValueInfo(intValue);
					if (obj == null)
					{
						this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse enum type. Type = {0}, Value = {1}", new object[]
						{
							paramType,
							value
						}));
					}
					return obj;
				}
				if (paramType == typeof(int))
				{
					return int.Parse(value, CultureInfo.InvariantCulture);
				}
				if (paramType == typeof(double))
				{
					return double.Parse(value, CultureInfo.InvariantCulture);
				}
				if (paramType == typeof(ExDateTime))
				{
					return DateTimeUtilities.ParseIsoDate(value, this.EventHandler.OwaContext.SessionContext.TimeZone);
				}
				if (paramType == typeof(bool))
				{
					if (string.Equals(value, "0", StringComparison.Ordinal))
					{
						return false;
					}
					if (string.Equals(value, "1", StringComparison.Ordinal))
					{
						return true;
					}
					this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
					{
						paramType,
						value
					}));
				}
				else
				{
					if (paramType == typeof(StoreObjectId))
					{
						UserContext userContext = this.EventHandler.OwaContext.UserContext;
						return Utilities.CreateStoreObjectId(userContext.MailboxSession, value);
					}
					if (paramType == typeof(ADObjectId))
					{
						ADObjectId adobjectId = DirectoryAssistance.ParseADObjectId(value);
						if (adobjectId == null)
						{
							this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
							{
								paramType,
								value
							}));
						}
						return adobjectId;
					}
					if (paramType == typeof(DocumentLibraryObjectId))
					{
						UserContext userContext2 = this.EventHandler.OwaContext.UserContext;
						Uri uri;
						if (null == (uri = Utilities.TryParseUri(value)))
						{
							return null;
						}
						ClassifyResult[] array = null;
						OwaWindowsIdentity owaWindowsIdentity = userContext2.LogonIdentity as OwaWindowsIdentity;
						if (owaWindowsIdentity != null && owaWindowsIdentity.WindowsPrincipal != null)
						{
							array = LinkClassifier.ClassifyLinks(owaWindowsIdentity.WindowsPrincipal, new Uri[]
							{
								uri
							});
						}
						if (array == null || array.Length == 0)
						{
							return null;
						}
						return array[0].ObjectId;
					}
					else if (paramType == typeof(OwaStoreObjectId))
					{
						UserContext userContext3 = this.EventHandler.OwaContext.UserContext;
						if (OwaStoreObjectId.IsDummyArchiveFolder(value))
						{
							return userContext3.GetArchiveRootFolderId();
						}
						return OwaStoreObjectId.CreateFromString(value);
					}
					else
					{
						this.ThrowParserException("Internal error: unknown type");
					}
				}
			}
			catch (FormatException)
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
				{
					paramType,
					value
				}));
			}
			catch (OwaParsingErrorException)
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
				{
					paramType,
					value
				}));
			}
			catch (OverflowException)
			{
				this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Type overflow. Type = {0}, Value = {1}", new object[]
				{
					paramType,
					value
				}));
			}
			return null;
		}

		// Token: 0x040009E5 RID: 2533
		internal const int MaxStructFieldCount = 32;

		// Token: 0x040009E6 RID: 2534
		internal const int MaxParameterCount = 64;

		// Token: 0x040009E7 RID: 2535
		internal const int MaxParameterCountGet = 16;

		// Token: 0x040009E8 RID: 2536
		internal const int MaxArrayItemCount = 2000;

		// Token: 0x040009E9 RID: 2537
		protected const string BooleanTrue = "1";

		// Token: 0x040009EA RID: 2538
		protected const string BooleanFalse = "0";

		// Token: 0x040009EB RID: 2539
		private OwaEventHandlerBase eventHandler;

		// Token: 0x040009EC RID: 2540
		private Hashtable parameterTable;

		// Token: 0x040009ED RID: 2541
		private ulong setMask;
	}
}
