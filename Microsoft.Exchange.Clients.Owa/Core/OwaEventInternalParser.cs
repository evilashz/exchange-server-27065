using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200018B RID: 395
	internal sealed class OwaEventInternalParser : OwaEventParserBase
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		internal OwaEventInternalParser(OwaEventHandlerBase eventHandler) : base(eventHandler, 4)
		{
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0005C6B2 File Offset: 0x0005A8B2
		internal OwaEventInternalParser(OwaEventHandlerBase eventHandler, int parameterTableCapacity) : base(eventHandler, parameterTableCapacity)
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		protected override Hashtable ParseParameters()
		{
			Dictionary<string, object> internalHandlerParameters = OwaContext.Current.InternalHandlerParameters;
			if (internalHandlerParameters == null)
			{
				this.ThrowParserException("Internal parameters are not set");
			}
			foreach (string text in internalHandlerParameters.Keys)
			{
				object obj = internalHandlerParameters[text];
				OwaEventParameterAttribute paramInfo = base.GetParamInfo(text);
				Type type = obj.GetType();
				if (type != paramInfo.Type && !type.IsSubclassOf(paramInfo.Type))
				{
					this.ThrowParserException("Parameter is not of the correct type");
				}
				base.AddParameter(paramInfo, obj);
			}
			return base.ParameterTable;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0005C774 File Offset: 0x0005A974
		protected override void ThrowParserException(string description)
		{
			throw new OwaInvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid internal handler call. {0}", new object[]
			{
				(description != null) ? (" " + description) : string.Empty
			}), null, this);
		}
	}
}
