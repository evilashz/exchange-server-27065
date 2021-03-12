using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E2F RID: 3631
	internal abstract class ODataRequest
	{
		// Token: 0x06005D9F RID: 23967 RVA: 0x001239EC File Offset: 0x00121BEC
		public ODataRequest(ODataContext odataContext)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			this.ODataContext = odataContext;
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x00123A06 File Offset: 0x00121C06
		// (set) Token: 0x06005DA1 RID: 23969 RVA: 0x00123A0E File Offset: 0x00121C0E
		public ODataContext ODataContext { get; private set; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x00123A17 File Offset: 0x00121C17
		public ODataQueryOptions ODataQueryOptions
		{
			get
			{
				return this.ODataContext.ODataQueryOptions;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x00123A24 File Offset: 0x00121C24
		// (set) Token: 0x06005DA4 RID: 23972 RVA: 0x00123A2C File Offset: 0x00121C2C
		public string IfMatchETag { get; protected set; }

		// Token: 0x06005DA5 RID: 23973 RVA: 0x00123A38 File Offset: 0x00121C38
		public virtual void LoadFromHttpRequest()
		{
			string text = this.ODataContext.HttpContext.Request.Headers["If-Match"];
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("W/") || text.StartsWith("w/"))
				{
					text = text.Substring(2, text.Length - 2);
				}
				text = text.Trim(new char[]
				{
					'"'
				});
				this.IfMatchETag = text;
			}
		}

		// Token: 0x06005DA6 RID: 23974
		public abstract ODataCommand GetODataCommand();

		// Token: 0x06005DA7 RID: 23975 RVA: 0x00123AB2 File Offset: 0x00121CB2
		public virtual void Validate()
		{
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x00123AB4 File Offset: 0x00121CB4
		public virtual string GetOperationNameForLogging()
		{
			string name = base.GetType().Name;
			int length = name.IndexOf("Request");
			return string.Format("OData:{0}", name.Substring(0, length));
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x00123AED File Offset: 0x00121CED
		public virtual void PerformAdditionalGrantCheck(string[] grantPresented)
		{
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x00123AF0 File Offset: 0x00121CF0
		protected TEntity ReadPostBodyAsEntity<TEntity>() where TEntity : Entity
		{
			RequestMessageReader requestMessageReader = new RequestMessageReader(this.ODataContext);
			return (TEntity)((object)requestMessageReader.ReadPostEntity());
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x00123B14 File Offset: 0x00121D14
		protected IDictionary<string, object> ReadPostBodyAsParameters()
		{
			RequestMessageReader requestMessageReader = new RequestMessageReader(this.ODataContext);
			return requestMessageReader.ReadPostParameters();
		}
	}
}
