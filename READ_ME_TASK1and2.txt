
/****************************************/
CHANGE LOG
/****************************************/
(Common)
- script change - search 2019-08-dd
- work: analog Product.AllowCustomerReviews, ManufacturerPartNumber
- id's: github ivkog karnekaj001


------------
(future ...)
(2019-08-02 18:20 note doma)
- TBD
- ProductDetailsModel
- Zgledi:
    - sku
    - ShowVendorOnProductDetailsPage
(2019-08-01)
- TBD
- sql upgrade scripts: see D:\___dev\_nopComm\nopc420---task1and2---\upgradescripts\4.20-tasks 1 and 2

-----------
(TBDS)
- ...
- @T("Products.Author") LocalizedString
- VRNI KAR UMIK IZ _CreateOrUpdate.Info.cshtml, glej P001

-----------
(PRILOGE)
P001
UMIK:
<div class="panel panel-default margin-bottom" id="group-associated-products">
            <div class="panel-heading">
                @T("Admin.Catalog.Products.AssociatedProducts")
            </div>
            @if (Model.Id > 0)
            {
                <div class="panel-body">
                    <ul class="common-list">
                        <li>
                            @T("Admin.Catalog.Products.AssociatedProducts.Note1")
                        </li>
                        <li>
                            @T("Admin.Catalog.Products.AssociatedProducts.Note2")
                        </li>
                    </ul>

                    @await Html.PartialAsync("Table", new DataTablesModel
                    {
                        Name = "associatedproducts-grid",
                        UrlRead = new DataUrl("AssociatedProductList", "Product", new RouteValueDictionary { [nameof(Model.AssociatedProductSearchModel.ProductId)] = Model.AssociatedProductSearchModel.ProductId }),
                        UrlDelete = new DataUrl("AssociatedProductDelete", "Product", null),
                        UrlUpdate = new DataUrl("AssociatedProductUpdate", "Product", null),
                        Length = Model.AssociatedProductSearchModel.PageSize,
                        LengthMenu = Model.AssociatedProductSearchModel.AvailablePageSizes,
                        ColumnCollection = new List<ColumnProperty>
                            {
                                Title = T("Admin.Catalog.Products.AssociatedProducts.Fields.Product").Text
                                },
                                new ColumnProperty(nameof(AssociatedProductModel.DisplayOrder))
                                {
                                    Title = T("Admin.Catalog.Products.AssociatedProducts.Fields.DisplayOrder").Text,
                                    Width = "150",
                                    ClassName = NopColumnClassDefaults.CenterAll,
                                    Editable = true,
                                    EditType = EditType.Number
                                },
                                new ColumnProperty(nameof(AssociatedProductModel.Id))
                                {
                                    Title = T("Admin.Common.View").Text,
                                    Width = "150",
                                    ClassName = NopColumnClassDefaults.Button,
                                    Render = new RenderButtonView(new DataUrl("~/Admin/Product/Edit/", nameof(AssociatedProductModel.Id)))
                                },
                                new ColumnProperty(nameof(AssociatedProductModel.Id))
                                {
                                    Title = T("Admin.Common.Edit").Text,
                                    Width = "200",
                                    ClassName = NopColumnClassDefaults.Button,
                                    Render = new RenderButtonsInlineEdit()
                                },
                                new ColumnProperty(nameof(AssociatedProductModel.Id))
                                {
                                    Title = T("Admin.Common.Delete").Text,
                                    Width = "100",
                                    Render = new RenderButtonRemove(T("Admin.Common.Delete").Text),
                                    ClassName = NopColumnClassDefaults.Button
                                }
                            }
                    })
                </div>
                <div class="panel-footer">
                    <button type="submit" id="btnAddNewAssociatedProduct" onclick="javascript:OpenWindow('@(Url.Action("AssociatedProductAddPopup", "Product", new {productId = Model.Id, btnId = "btnRefreshAssociatedProducts", formId = "product-form"}))', 800, 800, true); return false;" class="btn btn-primary">
                        @T("Admin.Catalog.Products.AssociatedProducts.AddNew")
                    </button>
                    <input type="submit" id="btnRefreshAssociatedProducts" style="display: none" />
                    <script>
                        $(document).ready(function () {
                            $('#btnRefreshAssociatedProducts').click(function () {
                                //refresh grid
                                updateTable('#associatedproducts-grid');

                                //return false to don't reload a page
                                return false;
                            });
                        });
                    </script>
                </div>
            }
            else
            {
    <div class="panel-body">
        @T("Admin.Catalog.Products.AssociatedProducts.SaveBeforeEdit")
    </div>
}
        </div>
