import { instance } from "@viz-js/viz";


window.vizRenderGraph = function (data) {
    instance().then(viz => {
        const svg = viz.renderSVGElement(data);

        document.getElementById("huffman-encoder-component-summary-huffman-tree").innerHTML = "";
        document.getElementById("huffman-encoder-component-summary-huffman-tree").appendChild(svg);

    });
}