// 해당 버튼 요소를 선택합니다.
const button_next = document.querySelector('.btn.btn-primary.w-full.text-18.h-14');
const page_card = document.querySelector('#__nuxt');


page_card.setAttribute('data-ga-page', JSON.stringify({
    title: "카드>{{신용카드,체크카드}}>카드정보>카드유형선택", 
    id: "CD_1_1_1B_2", 
    frame: "SPA", 
    card_name: "{{카드명}}", 
    card_code: "{{가상카드코드}}", 
    card_new_or_exist: "{{신규 or 기존}}", 
    card_type: "{{완편 OR 중편 OR 단편}}", 
    
    prod_funnel_name: "카드선택"
}));
button_next.setAttribute('data-ga-event', JSON.stringify({ text: "다음" }));

