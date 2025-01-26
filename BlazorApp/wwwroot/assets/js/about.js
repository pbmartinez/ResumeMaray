/**
 * Animate the skills items on reveal
 */
let skillsAnimation = document.querySelectorAll('.skills-animation');
skillsAnimation.forEach((item) => {
    new Waypoint({
        element: item,
        offset: '80%',
        handler: function (direction) {
            let progress = item.querySelectorAll('.progress .progress-bar');
            progress.forEach(el => {
                el.style.width = el.getAttribute('aria-valuenow') + '%';
            });
        }
    });
});

/**
   * Initiate Pure Counter
   */
new PureCounter();

/**
 * Init swiper sliders
 */
function initSwiper() {
    document.querySelectorAll(".init-swiper").forEach(function (swiperElement) {
        let config = JSON.parse(
            swiperElement.querySelector(".swiper-config").innerHTML.trim()
        );

        if (swiperElement.classList.contains("swiper-tab")) {
            initSwiperWithCustomPagination(swiperElement, config);
        } else {
            new Swiper(swiperElement, config);
        }
    });
}

window.addEventListener("load", initSwiper);
    