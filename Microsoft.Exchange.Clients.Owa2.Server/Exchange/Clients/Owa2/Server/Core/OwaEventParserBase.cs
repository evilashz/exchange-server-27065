using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001DE RID: 478
	internal abstract class OwaEventParserBase
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x000405A3 File Offset: 0x0003E7A3
		internal OwaEventParserBase(OwaEventHandlerBase eventHandler, int parameterTableCapacity)
		{
			this.eventHandler = eventHandler;
			this.parameterTable = new Hashtable(parameterTableCapacity);
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x000405BE File Offset: 0x0003E7BE
		protected Hashtable ParameterTable
		{
			get
			{
				return this.parameterTable;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000405C6 File Offset: 0x0003E7C6
		protected ulong SetMask
		{
			get
			{
				return this.setMask;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000405CE File Offset: 0x0003E7CE
		protected OwaEventHandlerBase EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000405D8 File Offset: 0x0003E7D8
		internal Hashtable Parse()
		{
			Hashtable result = this.ParseParameters();
			if ((this.EventHandler.EventInfo.RequiredMask & this.SetMask) != this.EventHandler.EventInfo.RequiredMask)
			{
				this.ThrowParserException("A required parameter of the event wasn't set");
			}
			return result;
		}

		// Token: 0x060010F1 RID: 4337
		protected abstract Hashtable ParseParameters();

		// Token: 0x060010F2 RID: 4338
		protected abstract void ThrowParserException(string description);

		// Token: 0x060010F3 RID: 4339 RVA: 0x00040621 File Offset: 0x0003E821
		protected void ThrowParserException()
		{
			this.ThrowParserException(null);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004062C File Offset: 0x0003E82C
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

		// Token: 0x060010F5 RID: 4341 RVA: 0x00040670 File Offset: 0x0003E870
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

		// Token: 0x060010F6 RID: 4342 RVA: 0x000406F4 File Offset: 0x0003E8F4
		protected void AddSimpleTypeParameter(OwaEventParameterAttribute paramInfo, string value)
		{
			object obj = null;
			if (paramInfo.Type == typeof(string))
			{
				obj = value;
			}
			else if (paramInfo.Type == typeof(int))
			{
				obj = int.Parse(value, CultureInfo.InvariantCulture);
			}
			else if (paramInfo.Type == typeof(double))
			{
				obj = double.Parse(value, CultureInfo.InvariantCulture);
			}
			else if (paramInfo.Type == typeof(long))
			{
				obj = long.Parse(value, CultureInfo.InvariantCulture);
			}
			else if (paramInfo.Type == typeof(bool))
			{
				if (string.Equals(value, "0", StringComparison.Ordinal))
				{
					obj = false;
				}
				else if (string.Equals(value, "1", StringComparison.Ordinal))
				{
					obj = true;
				}
				else
				{
					this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
					{
						paramInfo.Type,
						value
					}));
				}
			}
			if (obj != null)
			{
				this.AddParameter(paramInfo, obj);
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00040820 File Offset: 0x0003EA20
		protected void AddArrayParameter(OwaEventParameterAttribute paramInfo, ArrayList itemArray)
		{
			Array value = itemArray.ToArray(paramInfo.Type);
			this.AddParameter(paramInfo, value);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00040842 File Offset: 0x0003EA42
		protected void AddEmptyParameter(OwaEventParameterAttribute paramInfo)
		{
			if (paramInfo.IsArray)
			{
				this.AddArrayParameter(paramInfo, new ArrayList());
				return;
			}
			this.AddSimpleTypeParameter(paramInfo, string.Empty);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00040868 File Offset: 0x0003EA68
		protected void AddSimpleTypeToArray(OwaEventParameterAttribute paramInfo, ArrayList itemArray, string value)
		{
			if (paramInfo.Type == typeof(string))
			{
				this.AddItemToArray(itemArray, value);
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00040898 File Offset: 0x0003EA98
		protected void AddEmptyItemToArray(OwaEventParameterAttribute paramInfo, ArrayList itemArray)
		{
			this.AddSimpleTypeParameter(paramInfo, string.Empty);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000408A6 File Offset: 0x0003EAA6
		private void AddItemToArray(ArrayList itemArray, object value)
		{
			if (itemArray.Count >= 2000)
			{
				this.ThrowParserException("Reached maximum number of items in an array");
			}
			itemArray.Add(value);
		}

		// Token: 0x040009FE RID: 2558
		internal const int MaxStructFieldCount = 32;

		// Token: 0x040009FF RID: 2559
		internal const int MaxParameterCount = 64;

		// Token: 0x04000A00 RID: 2560
		internal const int MaxParameterCountGet = 16;

		// Token: 0x04000A01 RID: 2561
		internal const int MaxArrayItemCount = 2000;

		// Token: 0x04000A02 RID: 2562
		protected const string BooleanTrue = "1";

		// Token: 0x04000A03 RID: 2563
		protected const string BooleanFalse = "0";

		// Token: 0x04000A04 RID: 2564
		private OwaEventHandlerBase eventHandler;

		// Token: 0x04000A05 RID: 2565
		private Hashtable parameterTable;

		// Token: 0x04000A06 RID: 2566
		private ulong setMask;
	}
}
