(function (element) {
    var dataSections = [];
    // get pageInfo
    var pageElement = document.querySelector('[data-gtm-page]');
    if (!pageElement) {
        return null;
    }
    var pageData =  pageElement.dataset.gtmBody ? JSON.parse(pageElement.dataset.gtmBody) : null;

    // click event가 실행된 element
    var eventElement = element.closest('[data-gtm-event]');
    if (!eventElement) {
        return null;
    }
    var eventData = eventElement.dataset.gtmBody ? JSON.parse(eventElement.dataset.gtmBody) : null;
    if (!eventData) {
        return null;
    }

    // GET SECTION NAME
    var currentElement = eventElement;

    while (currentElement) {
        if (currentElement.dataset.gtmPage) {
        break;
        }
        var _section = currentElement.dataset.gtmSection;
        if (_section) {
        dataSections.push(_section);
        }
        currentElement = currentElement.parentElement;
    }

    var section = dataSections.reverse().join('-');

    var keyMapping = {
        // data-gtm-page - data-gtm-body
        gtm_title: "page_title",   
        gtm_id: "ep_cd57_screenid", 
        gtm_frame: "ep_cd11_native_yn", // "SPA" 고정값
        gtm_card_name: "ep_card_name",
        gtm_card_code: "ep_card_code",
        gtm_card_new_or_exist: "ep_cd64_card_apply_code",
        gtm_card_type: "ep_cd65_card_apply_kind",
        gtm_fin_prod_name: "ep_cd13_fn_pd_nm",
        gtm_fin_amount: "ep_cd17_fn_loan_amt",
        gtm_revol_rate: "ep_cd19_rvo_egm_stt_rt",
        gtm_revol_term: "ep_cd20_rvo_egm_stt_te",
        gtm_prod_funnel_name: "ep_cd48_pd_apply_nm",
        
        // data-gtm-section - data-gtm-body
        gtm_section: "ep_event_area",

        //data-gtm-click & data-gtm-visiblity - data-gtm-body
        gtm_event_name: "event_name",
        gtm_text: "gtm_text",
        gtm_frame: "ep_cd11_native_yn",
        gtm_search_keyword: "ep_cd25_srch_keyword",
        gtm_search_type: "ep_srch_keyword_type",
        gtm_search_result: "ep_cd26_srch_result",
        gtm_search_result_click: "ep_cd27_srch_res_clk_nm",
        gtm_card_name: "ep_card_name",
        gtm_card_code: "ep_card_code",
        gtm_card_new_or_exist: "ep_cd64_card_apply_code",
        gtm_card_type: "ep_cd65_card_apply_kind",
        gtm_fin_prod_name: "ep_cd13_fn_pd_nm",
        gtm_fin_amount: "ep_cd17_fn_loan_amt",
        gtm_revol_rate: "ep_cd19_rvo_egm_stt_rt",
        gtm_revol_term: "ep_cd20_rvo_egm_stt_te",
        gtm_prod_funnel_name: "ep_cd48_pd_apply_nm",
        gtm_cts_name: "ep_cd14_cts_nm",
        gtm_gtm_cts_id: "ep_cd42_cts_id",
        gtm_cts_sub_id: "ep_cd79_sub_cts_id",
        gtm_vertical_index: "ep_vertical_index",
        gtm_horizontal_index: "ep_horizontal_index",
        gtm_extra_index: "extra_index"
    };

    var output = {};

    for (var key in pageData) {
        if (keyMapping[key]) {
            output[keyMapping[key]] = pageData[key];
        }
    }
    console.log(output)
    dataLayer.push(output)

    return output;
})({{Click Element}})

