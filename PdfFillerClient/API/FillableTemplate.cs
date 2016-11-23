using System.Net;
using PdfFillerClient.DTO.FillableTemplate;
using PdfFillerClient.Exceptions;
using System.Collections.Generic;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with fillable template API. Filling field need to be changed for appropriate.
    /// API describring located here http://developers.pdffiller.com/#fillable-templates
    /// </summary>
    public class FillableTemplate
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/fillable_template";

        public FillableTemplate(IApiClient apiClientInstance)
        {
            _apiClient = apiClientInstance;
        }

        /// <summary>
        /// Populates a fillable form template which was pre-created in PDFFiller fillable constructor.
        /// </summary>
        /// <param name="newFillableTemplate">Filled FillableTemplateCreateRequest object.</param>
        /// <returns>Returns created document object info with additional data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillableTemplatePopulateResponse PopulateFillableTemplate(FillableTemplatePopulateRequest newFillableTemplate)
        {
            var response = _apiClient.Call(ApiPath, "POST", newFillableTemplate);
            var fillableTemplateInfo = _apiClient.GetResponseBody<FillableTemplatePopulateResponse>(response);
            return fillableTemplateInfo;
        }

        /// <summary>
        /// Lists created fillable templates.
        /// </summary>
        /// <returns>Returns fillable template documents list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillableTemplateListResponse GetFillableTemplatesList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var fillableTemplatesDocList = _apiClient.GetResponseBody<FillableTemplateListResponse>(response);
            return fillableTemplatesDocList;
        }

        /// <summary>
        /// Gets information about the fillable template fields.
        /// </summary>
        /// <param name="fillableTemplateId">Fillable template id.</param>
        /// <returns>Returns fillable template fields information.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public List<FillableTemplateFields> GetFillableTemplateInfo(long fillableTemplateId)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillableTemplateId, "GET", null);
            var callbackInfo = _apiClient.GetResponseBody<List<FillableTemplateFields>>(response);
            return callbackInfo;
        }

        /// <summary>
        /// Downloads the filled document by its identifier.
        /// </summary>
        /// <param name="filledTemplateId">Filled template document id.</param>
        /// <returns>Returns downloaded filled template document.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public byte[] DownloadFillableTemplate(long filledTemplateId)
        {
            byte[] response = _apiClient.DownloadFile(ApiPath + "/" + filledTemplateId + "/download");
            return response;
        }
    }
}
